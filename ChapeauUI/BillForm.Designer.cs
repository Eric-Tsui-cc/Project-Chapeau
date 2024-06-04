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
            receiptChapeau = new Label();
            receiptStreet = new Label();
            receiptPostCode = new Label();
            itemsListBox = new ListView();
            BtnBack = new Button();
            label12 = new Label();
            lblReceiptAountDisplay = new Label();
            lblTotalWithoutVAT = new Label();
            lblVAT = new Label();
            lblTotalVATDisplay = new Label();
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
            pictureBox1.Location = new Point(70, 13);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(301, 149);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // pnlReceipt
            // 
            pnlReceipt.Controls.Add(lblTotalVATDisplay);
            pnlReceipt.Controls.Add(lblVAT);
            pnlReceipt.Controls.Add(lblTotalWithoutVAT);
            pnlReceipt.Controls.Add(lblReceiptAountDisplay);
            pnlReceipt.Controls.Add(label12);
            pnlReceipt.Controls.Add(itemsListBox);
            pnlReceipt.Controls.Add(receiptPostCode);
            pnlReceipt.Controls.Add(receiptStreet);
            pnlReceipt.Controls.Add(receiptChapeau);
            pnlReceipt.Controls.Add(pictureBox1);
            pnlReceipt.Location = new Point(454, 28);
            pnlReceipt.Name = "pnlReceipt";
            pnlReceipt.Size = new Size(420, 589);
            pnlReceipt.TabIndex = 2;
            pnlReceipt.Paint += panel1_Paint;
            // 
            // receiptChapeau
            // 
            receiptChapeau.AutoSize = true;
            receiptChapeau.Location = new Point(190, 168);
            receiptChapeau.Name = "receiptChapeau";
            receiptChapeau.Size = new Size(67, 20);
            receiptChapeau.TabIndex = 2;
            receiptChapeau.Text = "Chapeau";
            // 
            // receiptStreet
            // 
            receiptStreet.AutoSize = true;
            receiptStreet.Location = new Point(158, 185);
            receiptStreet.Name = "receiptStreet";
            receiptStreet.Size = new Size(137, 20);
            receiptStreet.TabIndex = 3;
            receiptStreet.Text = "Blomendaalweg 12";
            // 
            // receiptPostCode
            // 
            receiptPostCode.AutoSize = true;
            receiptPostCode.Location = new Point(159, 205);
            receiptPostCode.Name = "receiptPostCode";
            receiptPostCode.Size = new Size(122, 20);
            receiptPostCode.TabIndex = 4;
            receiptPostCode.Text = "1234 AB Zeeland";
            // 
            // itemsListBox
            // 
            itemsListBox.Location = new Point(44, 230);
            itemsListBox.Name = "itemsListBox";
            itemsListBox.Size = new Size(340, 193);
            itemsListBox.TabIndex = 36;
            itemsListBox.UseCompatibleStateImageBehavior = false;
            // 
            // BtnBack
            // 
            BtnBack.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            BtnBack.Location = new Point(12, 28);
            BtnBack.Name = "BtnBack";
            BtnBack.Size = new Size(103, 43);
            BtnBack.TabIndex = 27;
            BtnBack.Text = "Back";
            BtnBack.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(285, 426);
            label12.Name = "label12";
            label12.Size = new Size(99, 20);
            label12.TabIndex = 37;
            label12.Text = "_______________";
            label12.Click += label12_Click;
            // 
            // lblReceiptAountDisplay
            // 
            lblReceiptAountDisplay.AutoSize = true;
            lblReceiptAountDisplay.Location = new Point(322, 446);
            lblReceiptAountDisplay.Name = "lblReceiptAountDisplay";
            lblReceiptAountDisplay.Size = new Size(36, 20);
            lblReceiptAountDisplay.TabIndex = 38;
            lblReceiptAountDisplay.Text = "0,00";
            // 
            // lblTotalWithoutVAT
            // 
            lblTotalWithoutVAT.AutoSize = true;
            lblTotalWithoutVAT.Location = new Point(44, 446);
            lblTotalWithoutVAT.Name = "lblTotalWithoutVAT";
            lblTotalWithoutVAT.Size = new Size(138, 20);
            lblTotalWithoutVAT.TabIndex = 39;
            lblTotalWithoutVAT.Text = "Total (Without VAT)";
            // 
            // lblVAT
            // 
            lblVAT.AutoSize = true;
            lblVAT.Location = new Point(51, 481);
            lblVAT.Name = "lblVAT";
            lblVAT.Size = new Size(114, 20);
            lblVAT.TabIndex = 40;
            lblVAT.Text = "VAT                    ";
            // 
            // lblTotalVATDisplay
            // 
            lblTotalVATDisplay.AutoSize = true;
            lblTotalVATDisplay.Location = new Point(322, 481);
            lblTotalVATDisplay.Name = "lblTotalVATDisplay";
            lblTotalVATDisplay.Size = new Size(36, 20);
            lblTotalVATDisplay.TabIndex = 41;
            lblTotalVATDisplay.Text = "0,00";
            // 
            // Bill
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1132, 689);
            Controls.Add(BtnBack);
            Controls.Add(pnlReceipt);
            Controls.Add(label1);
            Name = "Bill";
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
        private ListView itemsListBox;
        private Label receiptPostCode;
        private Label receiptStreet;
        private Label receiptChapeau;
        private Button BtnBack;
        private Label lblTotalWithoutVAT;
        private Label lblReceiptAountDisplay;
        private Label lblTotalVATDisplay;
        private Label lblVAT;
    }
}