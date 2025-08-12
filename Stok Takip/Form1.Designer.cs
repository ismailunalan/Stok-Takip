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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            listView1 = new ListView();
            button4 = new Button();
            textBox5 = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 16F);
            button1.Location = new Point(884, 323);
            button1.Name = "button1";
            button1.Size = new Size(128, 60);
            button1.TabIndex = 0;
            button1.Text = "Save";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 16F);
            button2.Location = new Point(733, 323);
            button2.Name = "button2";
            button2.Size = new Size(128, 60);
            button2.TabIndex = 5;
            button2.Text = "Clear";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 12F);
            button3.Location = new Point(41, 366);
            button3.Name = "button3";
            button3.Size = new Size(174, 44);
            button3.TabIndex = 6;
            button3.Text = "List products";
            button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(41, 58);
            label1.Name = "label1";
            label1.Size = new Size(173, 38);
            label1.TabIndex = 7;
            label1.Text = "Stock code  :";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.Location = new Point(41, 120);
            label2.Name = "label2";
            label2.Size = new Size(174, 38);
            label2.TabIndex = 8;
            label2.Text = "Stock name :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14F);
            label3.Location = new Point(41, 179);
            label3.Name = "label3";
            label3.Size = new Size(172, 38);
            label3.TabIndex = 9;
            label3.Text = "Barcod        :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 14F);
            label4.Location = new Point(41, 239);
            label4.Name = "label4";
            label4.Size = new Size(173, 38);
            label4.TabIndex = 10;
            label4.Text = "Shelf no      :";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(212, 66);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(248, 31);
            textBox1.TabIndex = 11;
            textBox1.TextChanged += textBox1_TextChanged_1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(212, 128);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(248, 31);
            textBox2.TabIndex = 12;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(211, 249);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(248, 31);
            textBox3.TabIndex = 14;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(211, 187);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(248, 31);
            textBox4.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 14F);
            label5.Location = new Point(562, 240);
            label5.Name = "label5";
            label5.Size = new Size(188, 38);
            label5.TabIndex = 18;
            label5.Text = "Price             :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 14F);
            label6.Location = new Point(562, 180);
            label6.Name = "label6";
            label6.Size = new Size(190, 38);
            label6.TabIndex = 17;
            label6.Text = "Tax rate         :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 14F);
            label7.Location = new Point(562, 121);
            label7.Name = "label7";
            label7.Size = new Size(191, 38);
            label7.TabIndex = 16;
            label7.Text = "Stock type     :";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 14F);
            label8.Location = new Point(562, 59);
            label8.Name = "label8";
            label8.Size = new Size(195, 38);
            label8.TabIndex = 15;
            label8.Text = "Stock group   :";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(763, 66);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(249, 33);
            comboBox1.TabIndex = 19;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(763, 129);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(249, 33);
            comboBox2.TabIndex = 20;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "% 8", "% 18" });
            comboBox3.Location = new Point(763, 187);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(249, 33);
            comboBox3.TabIndex = 21;
            comboBox3.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // listView1
            // 
            listView1.Location = new Point(41, 426);
            listView1.Name = "listView1";
            listView1.Size = new Size(971, 233);
            listView1.TabIndex = 24;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F);
            button4.Location = new Point(239, 366);
            button4.Name = "button4";
            button4.Size = new Size(187, 44);
            button4.TabIndex = 25;
            button4.Text = "Delete product";
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(763, 249);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(248, 31);
            textBox5.TabIndex = 26;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1058, 700);
            Controls.Add(textBox5);
            Controls.Add(button4);
            Controls.Add(listView1);
            Controls.Add(comboBox3);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(label5);
            Controls.Add(label6);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(textBox3);
            Controls.Add(textBox4);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ComboBox comboBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private ListView listView1;
        private Button button4;
        private TextBox textBox5;
    }
}
