namespace ChapeauUI
{
    partial class BillForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BillForm));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            pnlReceipt = new Panel();
            SubmitButton = new Button();
            textBox2 = new TextBox();
            label5 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            label4 = new Label();
            comboBox2 = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            comboBox1 = new ComboBox();
            labelVat = new Label();
            lblVAT = new Label();
            lblTotalWithoutVAT = new Label();
            labelTotalPrice = new Label();
            label12 = new Label();
            BtnBack = new Button();
            listView1 = new ListView();
            ORDERID = new ColumnHeader();
            ItemName = new ColumnHeader();
            Qty = new ColumnHeader();
            Price = new ColumnHeader();
            label6 = new Label();
            textBox3 = new TextBox();
            label7 = new Label();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlReceipt.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Location = new Point(1386, 11);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(64, 24);
            label1.TabIndex = 0;
            label1.Text = "Bill ID:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(48, 94);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(322, 154);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pnlReceipt
            // 
            pnlReceipt.BackColor = SystemColors.InactiveCaption;
            pnlReceipt.Controls.Add(label8);
            pnlReceipt.Controls.Add(label7);
            pnlReceipt.Controls.Add(textBox3);
            pnlReceipt.Controls.Add(label6);
            pnlReceipt.Controls.Add(SubmitButton);
            pnlReceipt.Controls.Add(textBox2);
            pnlReceipt.Controls.Add(label5);
            pnlReceipt.Controls.Add(button1);
            pnlReceipt.Controls.Add(textBox1);
            pnlReceipt.Controls.Add(label4);
            pnlReceipt.Controls.Add(comboBox2);
            pnlReceipt.Controls.Add(label3);
            pnlReceipt.Controls.Add(label2);
            pnlReceipt.Controls.Add(comboBox1);
            pnlReceipt.Controls.Add(labelVat);
            pnlReceipt.Controls.Add(lblVAT);
            pnlReceipt.Controls.Add(lblTotalWithoutVAT);
            pnlReceipt.Controls.Add(labelTotalPrice);
            pnlReceipt.Controls.Add(label12);
            pnlReceipt.Location = new Point(447, 51);
            pnlReceipt.Margin = new Padding(4);
            pnlReceipt.Name = "pnlReceipt";
            pnlReceipt.Size = new Size(564, 706);
            pnlReceipt.TabIndex = 2;
            pnlReceipt.Paint += panel1_Paint;
            // 
            // SubmitButton
            // 
            SubmitButton.Location = new Point(392, 359);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new Size(112, 34);
            SubmitButton.TabIndex = 51;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.HighlightText;
            textBox2.Location = new Point(49, 301);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(424, 30);
            textBox2.TabIndex = 50;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(49, 274);
            label5.Name = "label5";
            label5.Size = new Size(97, 24);
            label5.TabIndex = 49;
            label5.Text = "Feed Back";
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(171, 592);
            button1.Name = "button1";
            button1.Size = new Size(205, 74);
            button1.TabIndex = 48;
            button1.Text = "Finialize Order";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(226, 216);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 30);
            textBox1.TabIndex = 47;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(49, 216);
            label4.Name = "label4";
            label4.Size = new Size(37, 24);
            label4.TabIndex = 46;
            label4.Text = "Tip";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(226, 146);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(150, 32);
            comboBox2.TabIndex = 45;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(49, 154);
            label3.Name = "label3";
            label3.Size = new Size(160, 24);
            label3.TabIndex = 44;
            label3.Text = "Payment Method";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(49, 83);
            label2.Name = "label2";
            label2.Size = new Size(113, 28);
            label2.TabIndex = 43;
            label2.Text = "Table No.";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(226, 83);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(70, 32);
            comboBox1.TabIndex = 42;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // labelVat
            // 
            labelVat.AutoSize = true;
            labelVat.Location = new Point(426, 441);
            labelVat.Margin = new Padding(4, 0, 4, 0);
            labelVat.Name = "labelVat";
            labelVat.Size = new Size(47, 24);
            labelVat.TabIndex = 41;
            labelVat.Text = "0,00";
            // 
            // lblVAT
            // 
            lblVAT.AutoSize = true;
            lblVAT.Location = new Point(49, 441);
            lblVAT.Margin = new Padding(4, 0, 4, 0);
            lblVAT.Name = "lblVAT";
            lblVAT.Size = new Size(145, 24);
            lblVAT.TabIndex = 40;
            lblVAT.Text = "VAT                    ";
            // 
            // lblTotalWithoutVAT
            // 
            lblTotalWithoutVAT.AutoSize = true;
            lblTotalWithoutVAT.Location = new Point(49, 480);
            lblTotalWithoutVAT.Margin = new Padding(4, 0, 4, 0);
            lblTotalWithoutVAT.Name = "lblTotalWithoutVAT";
            lblTotalWithoutVAT.Size = new Size(180, 24);
            lblTotalWithoutVAT.TabIndex = 39;
            lblTotalWithoutVAT.Text = "Total (Without VAT)";
            // 
            // labelTotalPrice
            // 
            labelTotalPrice.AutoSize = true;
            labelTotalPrice.Location = new Point(426, 480);
            labelTotalPrice.Margin = new Padding(4, 0, 4, 0);
            labelTotalPrice.Name = "labelTotalPrice";
            labelTotalPrice.Size = new Size(47, 24);
            labelTotalPrice.TabIndex = 38;
            labelTotalPrice.Text = "0,00";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(392, 511);
            label12.Margin = new Padding(4, 0, 4, 0);
            label12.Name = "label12";
            label12.Size = new Size(0, 24);
            label12.TabIndex = 37;
            label12.Click += label12_Click;
            // 
            // BtnBack
            // 
            BtnBack.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            BtnBack.Location = new Point(48, 34);
            BtnBack.Margin = new Padding(4);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(142, 52);
            BtnBack.TabIndex = 27;
            BtnBack.Text = "Back";
            BtnBack.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { ORDERID, ItemName, Qty, Price });
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(16, 294);
            listView1.Name = "listView1";
            listView1.Size = new Size(385, 401);
            listView1.TabIndex = 28;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // ORDERID
            // 
            ORDERID.Text = "Id";
            ORDERID.Width = 25;
            // 
            // ItemName
            // 
            ItemName.Text = "Item Name";
            ItemName.Width = 120;
            // 
            // Qty
            // 
            Qty.Text = "Qty";
            Qty.Width = 40;
            // 
            // Price
            // 
            Price.Text = "Price";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(49, 364);
            label6.Name = "label6";
            label6.Size = new Size(129, 24);
            label6.TabIndex = 52;
            label6.Text = "Amout to pay";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(210, 361);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(150, 30);
            textBox3.TabIndex = 53;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(49, 520);
            label7.Name = "label7";
            label7.Size = new Size(181, 24);
            label7.TabIndex = 54;
            label7.Text = "Unpaid Amount left";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(426, 520);
            label8.Name = "label8";
            label8.Size = new Size(47, 24);
            label8.TabIndex = 55;
            label8.Text = "0.00";
            // 
            // BillForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1066, 827);
            Controls.Add(listView1);
            Controls.Add(BtnBack);
            Controls.Add(pnlReceipt);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Margin = new Padding(4);
            Name = "BillForm";
            Text = "Bill";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlReceipt.ResumeLayout(false);
            pnlReceipt.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Panel pnlReceipt;
        private Label label12;
        private Button BtnBack;
        private Label lblTotalWithoutVAT;
        private Label labelTotalPrice;
        private Label labelVat;
        private Label lblVAT;
        private Label label2;
        private ComboBox comboBox1;
        private TextBox textBox1;
        private Label label4;
        private ComboBox comboBox2;
        private Label label3;
        private ListView listView1;
        private ColumnHeader ORDERID;
        private ColumnHeader ItemName;
        private TextBox textBox2;
        private Label label5;
        private Button button1;
        private ColumnHeader Qty;
        private ColumnHeader Price;
        private Button SubmitButton;
        private Label label8;
        private Label label7;
        private TextBox textBox3;
        private Label label6;
    }
}