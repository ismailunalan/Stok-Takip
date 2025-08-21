using Stok_Takip.Models;
using Stok_Takip.Services;
using System.Xml.Linq;

namespace Stok_Takip
{
    public partial class Form1 : Form
    {
        internal string server = @"Server=localhost;Database=StokTakipDB;Integrated Security=True;TrustServerCertificate=True;";
        internal string url = "https://www.tcmb.gov.tr/kurlar/today.xml";

        private StockService stockService;
        private System.Windows.Forms.Timer timer1;


        public Form1()
        {
            InitializeComponent();
            stockService = new StockService(server);
        }

        public class ComboBoxItem
        {
            public string Display { get; set; }
            public string Value { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refreshList(clickedColumn, lastSort);
            setSearchBarState();
            setSearchByState();
            comboBox1.SelectedIndex = 0;
            currencyDisplayer(sender, e);
            if (productsGridView.Columns["ProductId"] != null)
            {
                productsGridView.Columns["ProductId"].Visible = fsagasga.Checked;
            }

        }

        private async void currencyDisplayer(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            currencyDisplay(sender, e);
            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 30000;
            timer1.Tick += currencyDisplay;
            timer1.Start();
        }

        private decimal usdRate = 0m;
        private decimal eurRate = 0m;
        private async void currencyDisplay(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                string response = await client.GetStringAsync(url);

                XDocument xmlDoc = XDocument.Parse(response);

                var usdNode = xmlDoc.Descendants("Currency").FirstOrDefault(x => x.Attribute("Kod")?.Value == "USD");
                var eurNode = xmlDoc.Descendants("Currency").FirstOrDefault(x => x.Attribute("Kod")?.Value == "EUR");

                if (usdNode != null)
                {
                    string usdForexSelling = usdNode.Element("ForexSelling")?.Value;
                    decimal.TryParse(usdForexSelling, System.Globalization.NumberStyles.Any,
                                     System.Globalization.CultureInfo.InvariantCulture, out usdRate);
                    usdBox.Text = usdRate.ToString("F4");
                }

                if (eurNode != null)
                {
                    string eurForexSelling = eurNode.Element("ForexSelling")?.Value;
                    decimal.TryParse(eurForexSelling, System.Globalization.NumberStyles.Any,
                                     System.Globalization.CultureInfo.InvariantCulture, out eurRate);
                    eurBox.Text = eurRate.ToString("F4");
                }
            }
        }

        private void setSearchBarState()
        {
            searchBar.ForeColor = Color.Gray;
            searchBar.Text = "Search...";

            searchBar.GotFocus += RemovePlaceholder;
            searchBar.LostFocus += SetPlaceholder;
        }

        private void RemovePlaceholder(object sender, EventArgs e)
        {
            if (searchBar.Text == "Search...")
            {
                searchBar.Text = "";
                searchBar.ForeColor = Color.Black;
            }
        }

        private void SetPlaceholder(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBar.Text))
            {
                searchBar.ForeColor = Color.Gray;
                searchBar.Text = "Search...";
            }
        }

        private void setSearchByState()
        {
            var columnOptions = new List<ComboBoxItem>
            {
                new ComboBoxItem { Display = "Stock Code", Value = "stock_code" },
                new ComboBoxItem { Display = "Stock Name", Value = "stock_name" },
                new ComboBoxItem { Display = "Barcode", Value = "barcode" },
                new ComboBoxItem { Display = "Shelf No", Value = "shelf_no" },
                new ComboBoxItem { Display = "Stock Group", Value = "stock_group" },
                new ComboBoxItem { Display = "Stock Type", Value = "stock_type" },
                new ComboBoxItem { Display = "Tax Rate", Value = "tax_rate" },
                new ComboBoxItem { Display = "Price", Value = "price" }
            };

            comboBox1.DataSource = columnOptions;
            comboBox1.DisplayMember = "Display";
            comboBox1.ValueMember = "Value";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Product p = new Product
            {
                ProductId = int.TryParse(hiddenIdLabel.Text, out int id) ? id : 0,
                StockCode = codeTextBox.Text,
                StockName = nameTextBox.Text,
                Barcode = string.IsNullOrWhiteSpace(barcodeTextBox.Text) ? null : barcodeTextBox.Text,
                ShelfNo = int.TryParse(shelfTextBox.Text, out int shelf) ? shelf : null,
                StockGroup = string.IsNullOrWhiteSpace(groupComboBox.Text) ? null : groupComboBox.Text,
                StockType = string.IsNullOrWhiteSpace(typeComboBox.Text) ? null : typeComboBox.Text,
                TaxRate = int.TryParse(taxComboBox.Text.Replace("%", ""), out int tax) ? tax : null,
                Price = decimal.TryParse(priceTextBox.Text, out decimal price) ? price : null
            };

            if (p.ProductId > 0)
            {
                stockService.updateProduct(p);
                cancelButton_Click(sender, e);
            }
            else
            {
                stockService.addProduct(p);
            }

            refreshList(clickedColumn, lastSort);
            clearBoxes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshList(clickedColumn, lastSort);
        }

        private void refreshList(string clickedColumn, bool lastSort)
        {
            string[] allowedColumns = { "product_id", "stock_code", "stock_name", "barcode", "shelf_no", "stock_group", "stock_type", "tax_rate", "price" };
            if (string.IsNullOrWhiteSpace(clickedColumn) || !allowedColumns.Contains(clickedColumn))
                clickedColumn = "product_id";

            List<Product> products = stockService.getProducts(
                clickedColumn,
                lastSort,
                comboBox1.SelectedValue?.ToString(),
                searchBar.Text != "Search..." ? searchBar.Text : null
            );

            productsGridView.DataSource = null;
            productsGridView.DataSource = products;

            //if (productsGridView.Columns["ProductId"] != null)
            //{
            //    productsGridView.Columns["ProductId"].Visible = false;
            //}

            productsGridView.ClearSelection();
        }

        private void clearBoxes()
        {
            hiddenIdLabel.Text = "0";
            codeTextBox.Clear();
            nameTextBox.Clear();
            shelfTextBox.Clear();
            barcodeTextBox.Clear();
            priceTextBox.Clear();

            groupComboBox.SelectedIndex = -1;
            typeComboBox.SelectedIndex = -1;
            taxComboBox.SelectedIndex = -1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearBoxes();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in productsGridView.SelectedRows)
            {
                int productId = Convert.ToInt32(row.Cells["ProductId"].Value);
                stockService.deleteProduct(productId);
            }
            refreshList(clickedColumn, lastSort);
        }

        private string lastSortColumn = "";
        private bool lastSort = true;
        string clickedColumn = "";

        private void productsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string[] columns = { "product_id", "stock_code", "stock_name", "barcode", "shelf_no", "stock_group", "stock_type", "tax_rate", "price" };
            clickedColumn = columns[e.ColumnIndex];

            if (lastSortColumn == clickedColumn)
            {
                lastSort = !lastSort;
            }
            else
            {
                lastSortColumn = clickedColumn;
                lastSort = true;
            }
            refreshList(clickedColumn, lastSort);
        }

        private void productsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (productsGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = productsGridView.SelectedRows[0];

                hiddenIdLabel.Text = selectedRow.Cells["ProductId"].Value?.ToString() ?? "";
                codeTextBox.Text = selectedRow.Cells["StockCode"].Value?.ToString() ?? "";
                nameTextBox.Text = selectedRow.Cells["StockName"].Value?.ToString() ?? "";
                barcodeTextBox.Text = selectedRow.Cells["Barcode"].Value?.ToString() ?? "";
                shelfTextBox.Text = selectedRow.Cells["ShelfNo"].Value?.ToString() ?? "";

                groupComboBox.Text = selectedRow.Cells["StockGroup"].Value?.ToString() ?? "";
                typeComboBox.Text = selectedRow.Cells["StockType"].Value?.ToString() ?? "";
                taxComboBox.Text = selectedRow.Cells["TaxRate"].Value != null ? $"%{selectedRow.Cells["TaxRate"].Value}" : "";
                priceTextBox.Text = selectedRow.Cells["Price"].Value?.ToString() ?? "";
            }
        }

        private void productsGridView_SelectionChanged_1(object sender, EventArgs e)
        {
            if (productsGridView.SelectedRows.Count > 0)
            {
                saveButton.Text = "Update";
                cancelButton.Visible = true;
                comboBox2.SelectedIndex = 0;
                FillProductForm(productsGridView.SelectedRows[0]);
            }
        }

        private void productsGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                productsGridView.ClearSelection();
                productsGridView.Rows[e.RowIndex].Selected = true;
                saveButton.Text = "Update";
                cancelButton.Visible = true;
                comboBox2.SelectedIndex = 0;
                FillProductForm(productsGridView.Rows[e.RowIndex]);
            }
        }

        private void FillProductForm(DataGridViewRow selectedRow)
        {
            if (selectedRow == null) return;

            hiddenIdLabel.Text = selectedRow.Cells["ProductId"].Value?.ToString() ?? "";
            codeTextBox.Text = selectedRow.Cells["StockCode"].Value?.ToString() ?? "";
            nameTextBox.Text = selectedRow.Cells["StockName"].Value?.ToString() ?? "";
            barcodeTextBox.Text = selectedRow.Cells["Barcode"].Value?.ToString() ?? "";
            shelfTextBox.Text = selectedRow.Cells["ShelfNo"].Value?.ToString() ?? "";
            priceTextBox.Text = selectedRow.Cells["Price"].Value?.ToString() ?? "";

            string groupValue = selectedRow.Cells["StockGroup"].Value?.ToString();
            if (!string.IsNullOrEmpty(groupValue))
            {
                int index = groupComboBox.Items.IndexOf(groupValue);
                if (index >= 0) groupComboBox.SelectedIndex = index;
            }

            string typeValue = selectedRow.Cells["StockType"].Value?.ToString();
            if (!string.IsNullOrEmpty(typeValue))
            {
                int index = typeComboBox.Items.IndexOf(typeValue);
                if (index >= 0) typeComboBox.SelectedIndex = index;
            }

            string taxValue = selectedRow.Cells["TaxRate"].Value?.ToString();
            if (!string.IsNullOrEmpty(taxValue))
            {
                int taxInt = (int)Math.Round(Convert.ToDecimal(taxValue.Trim()));
                foreach (var item in taxComboBox.Items)
                {
                    string itemText = item.ToString().Replace("%", "").Trim();
                    if (int.TryParse(itemText, out int itemTax) && itemTax == taxInt)
                    {
                        taxComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            productsGridView.Refresh();
        }

        private void productsGridView_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (productsGridView.Columns[e.ColumnIndex].Name == "Price" && e.Value != null && e.Value is decimal)
            {
                decimal val = (decimal)e.Value;

                if (comboBox2.SelectedIndex == 1 && usdRate > 0)
                {
                    e.Value = $"{(val / usdRate):F2} USD";
                    e.FormattingApplied = true;
                }
                else if (comboBox2.SelectedIndex == 2 && eurRate > 0)
                {
                    e.Value = $"{(val / eurRate):F2} EUR";
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = $"{val:F2} TL";
                    e.FormattingApplied = true;
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            productsGridView.ClearSelection();

            saveButton.Text = "Save";

            clearBoxes();

            hiddenIdLabel.Text = "";

            cancelButton.Visible = false;
        }

        private void idShowCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (productsGridView.Columns["ProductId"] != null)
            {
                productsGridView.Columns["ProductId"].Visible = idShowCheck.Checked;
            }
        }
    }
}