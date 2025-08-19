using Microsoft.Data.SqlClient;
using Stok_Takip.Models;
using Stok_Takip.Services;

namespace Stok_Takip
{
    public partial class Form1 : Form
    {
        internal string server = @"Server=localhost;Database=StokTakipDB;Integrated Security=True;TrustServerCertificate=True;";
        private StockService stockService;

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
                StockCode = codeTextBox.Text,
                StockName = nameTextBox.Text,
                Barcode = string.IsNullOrWhiteSpace(barcodeTextBox.Text) ? null : barcodeTextBox.Text,
                ShelfNo = int.TryParse(shelfTextBox.Text, out int shelf) ? shelf : null,
                StockGroup = string.IsNullOrWhiteSpace(groupComboBox.Text) ? null : groupComboBox.Text,
                StockType = string.IsNullOrWhiteSpace(typeComboBox.Text) ? null : typeComboBox.Text,
                TaxRate = int.TryParse(taxComboBox.Text.Replace("%", ""), out int tax) ? tax : null,
                Price = decimal.TryParse(priceTextBox.Text, out decimal price) ? price : null
            };

            if (!string.IsNullOrEmpty(codeTextBox.Text))
            {
                p.StockCode = codeTextBox.Text;



                stockService.updateProduct(p);
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
            string[] allowedColumns = { "stock_code", "stock_name", "barcode", "shelf_no", "stock_group", "stock_type", "tax_rate", "price" };
            if (string.IsNullOrWhiteSpace(clickedColumn) || !allowedColumns.Contains(clickedColumn))
                clickedColumn = "stock_code";

            List<Product> products = stockService.getProducts(
                clickedColumn,
                lastSort,
                comboBox1.SelectedValue?.ToString(),
                searchBar.Text != "Search..." ? searchBar.Text : null
            );

            productsGridView.DataSource = null;
            productsGridView.DataSource = products;

            productsGridView.ClearSelection(); // seçim temizle
        }

        private void clearBoxes()
        {
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
                string stockCode = row.Cells["StockCode"].Value.ToString();
                stockService.deleteProduct(stockCode);
            }
            refreshList(clickedColumn, lastSort);
        }

        private string lastSortColumn = "";
        private bool lastSort = true;
        string clickedColumn = "";

        private void productsGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string[] columns = { "stock_code", "stock_name", "barcode", "shelf_no", "stock_group", "stock_type", "tax_rate", "price" };
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
                DataGridViewRow selectedRow = productsGridView.SelectedRows[0];

                codeTextBox.Text = selectedRow.Cells["StockCode"].Value?.ToString() ?? "";
                nameTextBox.Text = selectedRow.Cells["StockName"].Value?.ToString() ?? "";
                barcodeTextBox.Text = selectedRow.Cells["Barcode"].Value?.ToString() ?? "";
                shelfTextBox.Text = selectedRow.Cells["ShelfNo"].Value?.ToString() ?? "";
                groupComboBox.Text = selectedRow.Cells["StockGroup"].Value?.ToString() ?? "";
                typeComboBox.Text = selectedRow.Cells["StockType"].Value?.ToString() ?? "";
                taxComboBox.Text = selectedRow.Cells["TaxRate"].Value?.ToString() ?? "";
                priceTextBox.Text = selectedRow.Cells["Price"].Value?.ToString() ?? "";

            }
        }

        private void productsGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {

                DataGridViewRow selectedRow = productsGridView.Rows[e.RowIndex];


                codeTextBox.Text = selectedRow.Cells["StockCode"].Value?.ToString() ?? "";
                nameTextBox.Text = selectedRow.Cells["StockName"].Value?.ToString() ?? "";
                barcodeTextBox.Text = selectedRow.Cells["Barcode"].Value?.ToString() ?? "";
                shelfTextBox.Text = selectedRow.Cells["ShelfNo"].Value?.ToString() ?? "";

                string groupValue = selectedRow.Cells["StockGroup"].Value?.ToString();
                if (!string.IsNullOrEmpty(groupValue))
                {
                    int index = groupComboBox.Items.IndexOf(groupValue);
                    groupComboBox.SelectedIndex = index; 
                }

                string typeValue = selectedRow.Cells["StockType"].Value?.ToString();
                if (!string.IsNullOrEmpty(typeValue))
                {
                    int index = typeComboBox.Items.IndexOf(typeValue);
                    typeComboBox.SelectedIndex = index;
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

                priceTextBox.Text = selectedRow.Cells["Price"].Value?.ToString() ?? "";

            }
        }
    }
}
