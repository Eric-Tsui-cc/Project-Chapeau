namespace ChapeauUI
{
    partial class MakeOrderPage
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
            Label CardOfDish;
            comboBox1 = new ComboBox();
            CategoryOfDish = new Label();
            QuantityOfDish = new Label();
            comboBox2 = new ComboBox();
            comboBox3 = new ComboBox();
            FinishOrderButton = new Button();
            Service = new Label();
            comboBox4 = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            TableOfOrder = new Label();
            comboBox5 = new ComboBox();
            AddItemButton = new Button();
            label1 = new Label();
            comboBox6 = new ComboBox();
            button1 = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            label2 = new Label();
            textBox1 = new TextBox();
            CardOfDish = new Label();
            SuspendLayout();
            // 
            // CardOfDish
            // 
            CardOfDish.Location = new Point(63, 103);
            CardOfDish.Name = "CardOfDish";
            CardOfDish.Size = new Size(94, 36);
            CardOfDish.TabIndex = 1;
            CardOfDish.Text = "Card";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(194, 100);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(182, 32);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // CategoryOfDish
            // 
            CategoryOfDish.AutoSize = true;
            CategoryOfDish.Location = new Point(63, 155);
            CategoryOfDish.Name = "CategoryOfDish";
            CategoryOfDish.Size = new Size(89, 24);
            CategoryOfDish.TabIndex = 2;
            CategoryOfDish.Text = "Category";
            // 
            // QuantityOfDish
            // 
            QuantityOfDish.AutoSize = true;
            QuantityOfDish.Location = new Point(63, 260);
            QuantityOfDish.Name = "QuantityOfDish";
            QuantityOfDish.Size = new Size(86, 24);
            QuantityOfDish.TabIndex = 3;
            QuantityOfDish.Text = "Quantity";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(194, 152);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(182, 32);
            comboBox2.TabIndex = 4;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(194, 260);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(182, 32);
            comboBox3.TabIndex = 5;
            // 
            // FinishOrderButton
            // 
            FinishOrderButton.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            FinishOrderButton.Location = new Point(247, 615);
            FinishOrderButton.Name = "FinishOrderButton";
            FinishOrderButton.Size = new Size(166, 74);
            FinishOrderButton.TabIndex = 6;
            FinishOrderButton.Text = "Place Order";
            FinishOrderButton.UseVisualStyleBackColor = true;
            FinishOrderButton.Click += FinishOrderButton_Click;
            // 
            // Service
            // 
            Service.AutoSize = true;
            Service.Location = new Point(63, 44);
            Service.Name = "Service";
            Service.Size = new Size(70, 24);
            Service.TabIndex = 7;
            Service.Text = "Service";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Location = new Point(139, 41);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(84, 32);
            comboBox4.TabIndex = 8;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(30, 423);
            label5.Name = "label5";
            label5.Size = new Size(99, 25);
            label5.TabIndex = 9;
            label5.Text = "Summary";
            // 
            // label6
            // 
            label6.Location = new Point(12, 448);
            label6.Name = "label6";
            label6.Size = new Size(419, 153);
            label6.TabIndex = 10;
            // 
            // TableOfOrder
            // 
            TableOfOrder.AutoSize = true;
            TableOfOrder.Location = new Point(239, 44);
            TableOfOrder.Name = "TableOfOrder";
            TableOfOrder.Size = new Size(57, 24);
            TableOfOrder.TabIndex = 11;
            TableOfOrder.Text = "Table";
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Location = new Point(313, 41);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(84, 32);
            comboBox5.TabIndex = 12;
            comboBox5.SelectedIndexChanged += comboBox5_SelectedIndexChanged;
            // 
            // AddItemButton
            // 
            AddItemButton.Location = new Point(238, 357);
            AddItemButton.Name = "AddItemButton";
            AddItemButton.Size = new Size(138, 73);
            AddItemButton.TabIndex = 13;
            AddItemButton.Text = "Add to the order";
            AddItemButton.UseVisualStyleBackColor = true;
            AddItemButton.Click += AddItemButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(63, 208);
            label1.Name = "label1";
            label1.Size = new Size(68, 24);
            label1.TabIndex = 14;
            label1.Text = "Object";
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Location = new Point(194, 205);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(182, 32);
            comboBox6.TabIndex = 15;
            // 
            // button1
            // 
            button1.Location = new Point(39, 615);
            button1.Name = "button1";
            button1.Size = new Size(118, 56);
            button1.TabIndex = 16;
            button1.Text = "Withdraw";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(63, 310);
            label2.Name = "label2";
            label2.Size = new Size(95, 24);
            label2.TabIndex = 17;
            label2.Text = "Comment";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(194, 307);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(182, 30);
            textBox1.TabIndex = 18;
            // 
            // MakeOrderPage
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(443, 701);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(comboBox6);
            Controls.Add(label1);
            Controls.Add(AddItemButton);
            Controls.Add(comboBox5);
            Controls.Add(TableOfOrder);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(comboBox4);
            Controls.Add(Service);
            Controls.Add(FinishOrderButton);
            Controls.Add(comboBox3);
            Controls.Add(comboBox2);
            Controls.Add(QuantityOfDish);
            Controls.Add(CategoryOfDish);
            Controls.Add(CardOfDish);
            Controls.Add(comboBox1);
            Name = "MakeOrderPage";
            Text = "MakeOrderPage";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label CategoryOfDish;
        private Label QuantityOfDish;
        private ComboBox comboBox2;
        private ComboBox comboBox3;
        private Button FinishOrderButton;
        private Label Service;
        private ComboBox comboBox4;
        private Label label5;
        private Label label6;
        private Label TableOfOrder;
        private ComboBox comboBox5;
        private Button AddItemButton;
        private Label label1;
        private ComboBox comboBox6;
        private Button button1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label label2;
        private TextBox textBox1;
    }
}