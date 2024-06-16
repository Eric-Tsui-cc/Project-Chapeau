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
            lblTotalamount = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            textBox3 = new TextBox();
            label6 = new Label();
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
            btnAddTip = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlReceipt.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Location = new Point(1008, 9);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 0;
            label1.Text = "Bill ID:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(35, 78);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(234, 128);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pnlReceipt
            // 
            pnlReceipt.BackColor = SystemColors.InactiveCaption;
            pnlReceipt.Controls.Add(btnAddTip);
            pnlReceipt.Controls.Add(lblTotalamount);
            pnlReceipt.Controls.Add(label10);
            pnlReceipt.Controls.Add(label9);
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
            pnlReceipt.Location = new Point(325, 42);
            pnlReceipt.Name = "pnlReceipt";
            pnlReceipt.Size = new Size(410, 720);
            pnlReceipt.TabIndex = 2;
            pnlReceipt.Paint += panel1_Paint;
            // 
            // lblTotalamount
            // 
            lblTotalamount.AutoSize = true;
            lblTotalamount.Location = new Point(311, 443);
            lblTotalamount.Name = "lblTotalamount";
            lblTotalamount.Size = new Size(36, 20);
            lblTotalamount.TabIndex = 58;
            lblTotalamount.Text = "0,00";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(34, 443);
            label10.Name = "label10";
            label10.Size = new Size(46, 20);
            label10.TabIndex = 57;
            label10.Text = "Total ";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(291, 420);
            label9.Name = "label9";
            label9.Size = new Size(81, 20);
            label9.TabIndex = 56;
            label9.Text = "------------";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(312, 498);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(36, 20);
            label8.TabIndex = 55;
            label8.Text = "0,00";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(34, 498);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(140, 20);
            label7.TabIndex = 54;
            label7.Text = "Unpaid Amount left";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(153, 301);
            textBox3.Margin = new Padding(2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(110, 27);
            textBox3.TabIndex = 53;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(36, 303);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(92, 20);
            label6.TabIndex = 52;
            label6.Text = "Going Dutch";
            // 
            // SubmitButton
            // 
            SubmitButton.Location = new Point(310, 301);
            SubmitButton.Margin = new Padding(2);
            SubmitButton.Name = "SubmitButton";
            SubmitButton.Size = new Size(81, 27);
            SubmitButton.TabIndex = 51;
            SubmitButton.Text = "Submit";
            SubmitButton.UseVisualStyleBackColor = true;
            SubmitButton.Click += button2_Click;
            // 
            // textBox2
            // 
            textBox2.BackColor = SystemColors.HighlightText;
            textBox2.Location = new Point(36, 251);
            textBox2.Margin = new Padding(2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(309, 27);
            textBox2.TabIndex = 50;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(36, 228);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(76, 20);
            label5.TabIndex = 49;
            label5.Text = "Feed Back";
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(255, 128, 0);
            button1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(153, 658);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(255, 62);
            button1.TabIndex = 48;
            button1.Text = "Finialize Payment";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(164, 180);
            textBox1.Margin = new Padding(2);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(110, 27);
            textBox1.TabIndex = 47;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(36, 180);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(30, 20);
            label4.TabIndex = 46;
            label4.Text = "Tip";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(164, 122);
            comboBox2.Margin = new Padding(2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(110, 28);
            comboBox2.TabIndex = 45;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(36, 128);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(121, 20);
            label3.TabIndex = 44;
            label3.Text = "Payment Method";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(36, 69);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(97, 25);
            label2.TabIndex = 43;
            label2.Text = "Table No.";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(164, 69);
            comboBox1.Margin = new Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(52, 28);
            comboBox1.TabIndex = 42;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // labelVat
            // 
            labelVat.AutoSize = true;
            labelVat.Location = new Point(310, 368);
            labelVat.Name = "labelVat";
            labelVat.Size = new Size(36, 20);
            labelVat.TabIndex = 41;
            labelVat.Text = "0,00";
            // 
            // lblVAT
            // 
            lblVAT.AutoSize = true;
            lblVAT.Location = new Point(36, 368);
            lblVAT.Name = "lblVAT";
            lblVAT.Size = new Size(114, 20);
            lblVAT.TabIndex = 40;
            lblVAT.Text = "VAT                    ";
            // 
            // lblTotalWithoutVAT
            // 
            lblTotalWithoutVAT.AutoSize = true;
            lblTotalWithoutVAT.Location = new Point(36, 400);
            lblTotalWithoutVAT.Name = "lblTotalWithoutVAT";
            lblTotalWithoutVAT.Size = new Size(138, 20);
            lblTotalWithoutVAT.TabIndex = 39;
            lblTotalWithoutVAT.Text = "Total (Without VAT)";
            // 
            // labelTotalPrice
            // 
            labelTotalPrice.AutoSize = true;
            labelTotalPrice.Location = new Point(310, 400);
            labelTotalPrice.Name = "labelTotalPrice";
            labelTotalPrice.Size = new Size(36, 20);
            labelTotalPrice.TabIndex = 38;
            labelTotalPrice.Text = "0,00";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(285, 426);
            label12.Name = "label12";
            label12.Size = new Size(0, 20);
            label12.TabIndex = 37;
            label12.Click += label12_Click;
            // 
            // BtnBack
            // 
            BtnBack.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            BtnBack.Location = new Point(35, 28);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(103, 43);
            BtnBack.TabIndex = 27;
            BtnBack.Text = "Back";
            BtnBack.UseVisualStyleBackColor = true;
            BtnBack.Click += BtnBack_Click;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { ORDERID, ItemName, Qty, Price });
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(12, 245);
            listView1.Margin = new Padding(2);
            listView1.Name = "listView1";
            listView1.Size = new Size(281, 335);
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
            // btnAddTip
            // 
            btnAddTip.Location = new Point(310, 180);
            btnAddTip.Margin = new Padding(2);
            btnAddTip.Name = "btnAddTip";
            btnAddTip.Size = new Size(81, 27);
            btnAddTip.TabIndex = 59;
            btnAddTip.Text = "Add Tip";
            btnAddTip.UseVisualStyleBackColor = true;
            // 
            // BillForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(775, 760);
            Controls.Add(listView1);
            Controls.Add(BtnBack);
            Controls.Add(pnlReceipt);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
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
        private Label label10;
        private Label label9;
        private Label lblTotalamount;
        private Button btnAddTip;
    }
}