using Stok_Takip.Models;
using Stok_Takip.Services;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stok_Takip
{
    public partial class Form1 : Form
    {
        internal string server = @"Server=localhost;Database=StokTakipDB;Integrated Security=True;TrustServerCertificate=True;";
        internal string url = "https://www.tcmb.gov.tr/kurlar/today.xml";

        private StockServices_APIVersion stockService_APIVersion;
        private System.Windows.Forms.Timer timer1;


        public Form1()
        {
            InitializeComponent();
            stockService_APIVersion = new StockServices_APIVersion();
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
            idShowCheck.Checked = true;
            if (productsGridView.Columns["Id"] != null)
            {
                productsGridView.Columns["Id"].Visible = idShowCheck.Checked;
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
                new ComboBoxItem { Display = "Stock Code", Value = "Code" },
                new ComboBoxItem { Display = "Stock Name", Value = "Name" },
                new ComboBoxItem { Display = "Barcode", Value = "Barcode" },
                new ComboBoxItem { Display = "Quantity", Value = "Quantity" },
                new ComboBoxItem { Display = "Stock Group", Value = "Group" },
                new ComboBoxItem { Display = "Stock Type", Value = "Type" },
                new ComboBoxItem { Display = "Tax Rate", Value = "TaxRate" },
                new ComboBoxItem { Display = "Price", Value = "Price" }
            };

            comboBox1.DataSource = columnOptions;
            comboBox1.DisplayMember = "Display";
            comboBox1.ValueMember = "Value";
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(codeTextBox.Text))
            {
                MessageBox.Show("Stok kodu boş olamaz.");
                return;
            }

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Stok adı boş olamaz.");
                return;
            }

            Product p = new Product
            {
                Id = int.TryParse(hiddenIdLabel.Text, out int id) ? id : 0,
                Code = codeTextBox.Text,
                Name = nameTextBox.Text,
                Barcode = string.IsNullOrWhiteSpace(barcodeTextBox.Text) ? null : barcodeTextBox.Text,
                Quantity = int.TryParse(quantityTextBox.Text, out int quantity) ? quantity : null,
                Group = string.IsNullOrWhiteSpace(groupComboBox.Text) ? null : groupComboBox.Text,
                Type = string.IsNullOrWhiteSpace(typeComboBox.Text) ? null : typeComboBox.Text,
                TaxRate = int.TryParse(taxComboBox.Text.Replace("%", ""), out int tax) ? tax : null,
                Price = decimal.TryParse(priceTextBox.Text, out decimal price) ? price : null
            };

            if (p.Id > 0)
            {
                await stockService_APIVersion.UpdateProductAsync(p);
                cancelButton_Click(sender, e);
                await refreshList(clickedColumn, lastSort);
            }
            else
            {
                await stockService_APIVersion.AddProductAsync(p);
                await refreshList(clickedColumn, lastSort);
            }

            clearBoxes();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await refreshList(clickedColumn, lastSort);
        }

        private async Task refreshList(string clickedColumn, bool lastSort)
        {
            string? searchText = (!string.IsNullOrWhiteSpace(searchBar.Text) && searchBar.Text != "Search...")
                ? searchBar.Text.Trim()
                : null;

            string? searchCol = null;
            if (comboBox1.SelectedItem is ComboBoxItem item && !string.IsNullOrWhiteSpace(item.Value))
                searchCol = item.Value;

            var products = await stockService_APIVersion.GetAllProductsAsync(
                sortColumn: string.IsNullOrWhiteSpace(clickedColumn) ? "Code" : clickedColumn,
                ascending: lastSort,
                searchColumn: searchCol,
                searchText: searchText
            );

            productsGridView.DataSource = products;
            productsGridView.ClearSelection();
        }



        private void clearBoxes()
        {
            hiddenIdLabel.Text = "0";
            codeTextBox.Clear();
            nameTextBox.Clear();
            quantityTextBox.Clear();
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
                int productId = Convert.ToInt32(row.Cells["Id"].Value);
                stockService_APIVersion.DeleteProductAsync(productId);
            }
            clearBoxes();
            refreshList(clickedColumn, lastSort);
        }

        private string lastSortColumn = "";
        private bool lastSort = true;
        string clickedColumn = "";

        private async void productsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string[] columns = { "Id", "Code", "Name", "Barcode", "Quantity", "Group", "Type", "TaxRate", "Price" };
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

            await refreshList(clickedColumn, lastSort);
        }


        private void productsGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (productsGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = productsGridView.SelectedRows[0];

                hiddenIdLabel.Text = selectedRow.Cells["Id"].Value?.ToString() ?? "";
                codeTextBox.Text = selectedRow.Cells["Code"].Value?.ToString() ?? "";
                nameTextBox.Text = selectedRow.Cells["Name"].Value?.ToString() ?? "";
                barcodeTextBox.Text = selectedRow.Cells["Barcode"].Value?.ToString() ?? "";
                quantityTextBox.Text = selectedRow.Cells["Quantity"].Value?.ToString() ?? "";
                groupComboBox.Text = selectedRow.Cells["Group"].Value?.ToString() ?? "";
                typeComboBox.Text = selectedRow.Cells["Type"].Value?.ToString() ?? "";
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

            hiddenIdLabel.Text = selectedRow.Cells["Id"].Value?.ToString() ?? "";
            codeTextBox.Text = selectedRow.Cells["Code"].Value?.ToString() ?? "";
            nameTextBox.Text = selectedRow.Cells["Name"].Value?.ToString() ?? "";
            barcodeTextBox.Text = selectedRow.Cells["Barcode"].Value?.ToString() ?? "";
            quantityTextBox.Text = selectedRow.Cells["Quantity"].Value?.ToString() ?? "";
            priceTextBox.Text = selectedRow.Cells["Price"].Value?.ToString() ?? "";

            string groupValue = selectedRow.Cells["Group"].Value?.ToString();
            if (!string.IsNullOrEmpty(groupValue))
            {
                int index = groupComboBox.Items.IndexOf(groupValue);
                if (index >= 0) groupComboBox.SelectedIndex = index;
            }

            string typeValue = selectedRow.Cells["Type"].Value?.ToString();
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
            if (productsGridView.Columns["Id"] != null)
            {
                productsGridView.Columns["Id"].Visible = idShowCheck.Checked;
            }
        }
    }
}