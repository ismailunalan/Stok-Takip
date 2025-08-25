namespace Stok_Takip
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            saveButton = new Button();
            clearButton = new Button();
            listButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            codeTextBox = new TextBox();
            nameTextBox = new TextBox();
            shelfTextBox = new TextBox();
            barcodeTextBox = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            groupComboBox = new ComboBox();
            typeComboBox = new ComboBox();
            taxComboBox = new ComboBox();
            deleteButton = new Button();
            priceTextBox = new TextBox();
            label9 = new Label();
            searchBar = new TextBox();
            comboBox1 = new ComboBox();
            productsGridView = new DataGridView();
            hiddenIdLabel = new Label();
            usdLabel = new Label();
            usdBox = new TextBox();
            eurBox = new TextBox();
            label10 = new Label();
            comboBox2 = new ComboBox();
            cancelButton = new Button();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            idShowCheck = new CheckBox();
=======
>>>>>>> parent of b531999 (API implementation completed. Show ID button added)
            ((System.ComponentModel.ISupportInitialize)productsGridView).BeginInit();
            SuspendLayout();
            // 
            // saveButton
            // 
            saveButton.Font = new Font("Segoe UI", 12F);
            saveButton.Location = new Point(729, 38);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(194, 73);
            saveButton.TabIndex = 0;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // clearButton
            // 
            clearButton.Font = new Font("Segoe UI", 12F);
            clearButton.Location = new Point(502, 38);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(194, 73);
            clearButton.TabIndex = 5;
            clearButton.Text = "Clear";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += button2_Click;
            // 
            // listButton
            // 
            listButton.Font = new Font("Segoe UI", 12F);
            listButton.Location = new Point(41, 38);
            listButton.Name = "listButton";
            listButton.Size = new Size(194, 73);
            listButton.TabIndex = 6;
            listButton.Text = "List products";
            listButton.UseVisualStyleBackColor = true;
            listButton.Click += button3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(41, 149);
            label1.Name = "label1";
            label1.Size = new Size(173, 38);
            label1.TabIndex = 7;
            label1.Text = "Stock code  :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(41, 211);
            label2.Name = "label2";
            label2.Size = new Size(174, 38);
            label2.TabIndex = 8;
            label2.Text = "Stock name :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(41, 270);
            label3.Name = "label3";
            label3.Size = new Size(25, 38);
            label3.TabIndex = 9;
            label3.Text = " ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(41, 330);
            label4.Name = "label4";
            label4.Size = new Size(173, 38);
            label4.TabIndex = 10;
            label4.Text = "Shelf no      :";
            // 
            // codeTextBox
            // 
            codeTextBox.Location = new Point(212, 157);
            codeTextBox.Name = "codeTextBox";
            codeTextBox.Size = new Size(242, 31);
            codeTextBox.TabIndex = 11;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(212, 219);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(242, 31);
            nameTextBox.TabIndex = 12;
            // 
            // shelfTextBox
            // 
            shelfTextBox.Location = new Point(211, 340);
            shelfTextBox.Name = "shelfTextBox";
            shelfTextBox.Size = new Size(242, 31);
            shelfTextBox.TabIndex = 14;
            // 
            // barcodeTextBox
            // 
            barcodeTextBox.Location = new Point(211, 278);
            barcodeTextBox.Name = "barcodeTextBox";
            barcodeTextBox.Size = new Size(242, 31);
            barcodeTextBox.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F);
            label5.Location = new Point(562, 330);
            label5.Name = "label5";
            label5.Size = new Size(188, 38);
            label5.TabIndex = 18;
            label5.Text = "Price             :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F);
            label6.Location = new Point(562, 271);
            label6.Name = "label6";
            label6.Size = new Size(190, 38);
            label6.TabIndex = 17;
            label6.Text = "Tax rate         :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F);
            label7.Location = new Point(562, 212);
            label7.Name = "label7";
            label7.Size = new Size(191, 38);
            label7.TabIndex = 16;
            label7.Text = "Stock type     :";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14F);
            label8.Location = new Point(562, 150);
            label8.Name = "label8";
            label8.Size = new Size(195, 38);
            label8.TabIndex = 15;
            label8.Text = "Stock group   :";
            // 
            // groupComboBox
            // 
            groupComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            groupComboBox.FormattingEnabled = true;
            groupComboBox.Items.AddRange(new object[] { "Kimyasal", "Yapı" });
            groupComboBox.Location = new Point(763, 157);
            groupComboBox.Name = "groupComboBox";
            groupComboBox.Size = new Size(243, 33);
            groupComboBox.TabIndex = 19;
            // 
            // typeComboBox
            // 
            typeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            typeComboBox.FormattingEnabled = true;
            typeComboBox.Items.AddRange(new object[] { "Su", "Kalem" });
            typeComboBox.Location = new Point(763, 220);
            typeComboBox.Name = "typeComboBox";
            typeComboBox.Size = new Size(243, 33);
            typeComboBox.TabIndex = 20;
            // 
            // taxComboBox
            // 
            taxComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            taxComboBox.FormattingEnabled = true;
            taxComboBox.Items.AddRange(new object[] { "% 8", "% 18" });
            taxComboBox.Location = new Point(763, 278);
            taxComboBox.Name = "taxComboBox";
            taxComboBox.Size = new Size(243, 33);
            taxComboBox.TabIndex = 21;
            // 
            // deleteButton
            // 
            deleteButton.Font = new Font("Segoe UI", 12F);
            deleteButton.Location = new Point(260, 38);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(212, 73);
            deleteButton.TabIndex = 25;
            deleteButton.Text = "Delete product(s)";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += button4_Click_1;
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(763, 340);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.Size = new Size(242, 31);
            priceTextBox.TabIndex = 26;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 14F);
            label9.Location = new Point(41, 271);
            label9.Name = "label9";
            label9.Size = new Size(171, 38);
            label9.TabIndex = 27;
            label9.Text = "Barcode      :";
            // 
            // searchBar
            // 
            searchBar.ForeColor = SystemColors.ControlDark;
            searchBar.Location = new Point(41, 389);
            searchBar.Name = "searchBar";
            searchBar.Size = new Size(239, 31);
            searchBar.TabIndex = 28;
            searchBar.Tag = "";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Stock Code", "Stock Name", "Barcode", "Shelf No", "Stock Group", "Stock Type", "Tax Rate", "Price" });
            comboBox1.Location = new Point(286, 389);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(129, 33);
            comboBox1.TabIndex = 29;
            // 
            // productsGridView
            // 
            productsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            productsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            productsGridView.Location = new Point(41, 428);
            productsGridView.Name = "productsGridView";
            productsGridView.ReadOnly = true;
            productsGridView.RowHeadersWidth = 62;
            productsGridView.Size = new Size(964, 233);
            productsGridView.TabIndex = 30;
            productsGridView.CellContentDoubleClick += productsGridView_CellContentDoubleClick;
            productsGridView.CellFormatting += productsGridView_CellFormatting_1;
            productsGridView.ColumnHeaderMouseClick += productsGridView_ColumnHeaderMouseClick;
            productsGridView.SelectionChanged += productsGridView_SelectionChanged_1;
            // 
            // hiddenIdLabel
            // 
            hiddenIdLabel.Location = new Point(41, 9);
            hiddenIdLabel.Name = "hiddenIdLabel";
            hiddenIdLabel.Size = new Size(10, 10);
            hiddenIdLabel.TabIndex = 31;
            hiddenIdLabel.Text = "label10";
            hiddenIdLabel.Visible = false;
            // 
            // usdLabel
            // 
            usdLabel.AutoSize = true;
            usdLabel.Location = new Point(625, 666);
            usdLabel.Name = "usdLabel";
            usdLabel.Size = new Size(113, 25);
            usdLabel.TabIndex = 32;
            usdLabel.Text = "USD Selling :";
            // 
            // usdBox
            // 
            usdBox.Enabled = false;
            usdBox.Location = new Point(744, 663);
            usdBox.Name = "usdBox";
            usdBox.Size = new Size(60, 31);
            usdBox.TabIndex = 33;
            // 
            // eurBox
            // 
            eurBox.Enabled = false;
            eurBox.Location = new Point(929, 663);
            eurBox.Name = "eurBox";
            eurBox.Size = new Size(60, 31);
            eurBox.TabIndex = 35;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(810, 666);
            label10.Name = "label10";
            label10.Size = new Size(110, 25);
            label10.TabIndex = 34;
            label10.Text = "EUR Selling :";
            // 
            // comboBox2
            // 
            comboBox2.BackColor = SystemColors.ActiveBorder;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.ForeColor = SystemColors.WindowFrame;
            comboBox2.FormattingEnabled = true;
            comboBox2.ItemHeight = 25;
            comboBox2.Items.AddRange(new object[] { "TL", "USD", "EUR" });
            comboBox2.Location = new Point(648, 335);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(70, 33);
            comboBox2.TabIndex = 36;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // cancelButton
            // 
            cancelButton.BackColor = Color.Transparent;
            cancelButton.Location = new Point(941, 38);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(78, 73);
            cancelButton.TabIndex = 37;
            cancelButton.Text = "Cancel\r\nEditing\r\n";
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Visible = false;
            cancelButton.Click += cancelButton_Click;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // idShowCheck
            // 
            idShowCheck.AutoSize = true;
            idShowCheck.Location = new Point(431, 391);
            idShowCheck.Name = "idShowCheck";
            idShowCheck.Size = new Size(105, 29);
            idShowCheck.TabIndex = 39;
            idShowCheck.Text = "Show ID";
            idShowCheck.UseVisualStyleBackColor = true;
            idShowCheck.CheckedChanged += idShowCheck_CheckedChanged;
            // 
=======
>>>>>>> parent of b531999 (API implementation completed. Show ID button added)
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1045, 700);
<<<<<<< HEAD
            Controls.Add(idShowCheck);
            Controls.Add(cancelButton);
            Controls.Add(comboBox2);
            Controls.Add(eurBox);
            Controls.Add(label10);
            Controls.Add(usdBox);
            Controls.Add(usdLabel);
            Controls.Add(hiddenIdLabel);
            Controls.Add(productsGridView);
            Controls.Add(comboBox1);
            Controls.Add(searchBar);
            Controls.Add(label9);
            Controls.Add(priceTextBox);
            Controls.Add(deleteButton);
            Controls.Add(taxComboBox);
            Controls.Add(typeComboBox);
            Controls.Add(groupComboBox);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(shelfTextBox);
            Controls.Add(barcodeTextBox);
            Controls.Add(nameTextBox);
            Controls.Add(codeTextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(listButton);
            Controls.Add(clearButton);
            Controls.Add(saveButton);
            Name = "Form1";
            Text = "Stok Takip";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)productsGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button saveButton;
        private Button clearButton;
        private Button listButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox codeTextBox;
        private TextBox nameTextBox;
        private TextBox shelfTextBox;
        private TextBox barcodeTextBox;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ComboBox groupComboBox;
        private ComboBox typeComboBox;
        private ComboBox taxComboBox;
        private Button deleteButton;
        private TextBox priceTextBox;
        private Label label9;
        private TextBox searchBar;
        private ComboBox comboBox1;
        private DataGridView productsGridView;
        private Label hiddenIdLabel;
        private Label usdLabel;
        private TextBox usdBox;
        private TextBox eurBox;
        private Label label10;
        private ComboBox comboBox2;
        private Button cancelButton;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private RadioButton fsagasga;
        private CheckBox idShowCheck;
    }
}
