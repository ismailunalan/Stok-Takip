namespace Stok_Takip
{
    public partial class Form1 : Form
    {
        private List<ProductClass> products = new List<ProductClass>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductClass product = new ProductClass();

            product.id = textBox1.Text;
            product.name = textBox2.Text;
            product.barcod = textBox3.Text;
            product.selfNo = Convert.ToInt32(textBox4.Text);
            product.group = comboBox1.SelectedIndex;
            product.type = comboBox2.SelectedIndex;
            product.taxRate = Convert.ToInt32(comboBox3.SelectedItem);
            product.price = Convert.ToInt32(textBox5.Text);

            products.Add(product);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
