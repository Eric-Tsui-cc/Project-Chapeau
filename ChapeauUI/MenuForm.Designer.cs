namespace ChapeauUI
{
    partial class MenuForm
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
            btnLunch = new Button();
            btnDiner = new Button();
            btnDrinks = new Button();
            ListViewMenu = new ListView();
            btnSendOrder = new RoundedButton();
            Overview = new Button();
            btnDelete = new Button();
            btnAdd = new Button();
            label1 = new Label();
            cmbCategory = new ComboBox();
            label2 = new Label();
            listViewOrderOrders = new ListView();
            cmbService = new ComboBox();
            cmbTableNo = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // btnLunch
            // 
            btnLunch.FlatStyle = FlatStyle.System;
            btnLunch.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnLunch.Location = new Point(60, 96);
            btnLunch.Name = "btnLunch";
            btnLunch.Size = new Size(109, 37);
            btnLunch.TabIndex = 0;
            btnLunch.Text = "Lunch";
            btnLunch.UseVisualStyleBackColor = true;
            // 
            // btnDiner
            // 
            btnDiner.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnDiner.Location = new Point(225, 96);
            btnDiner.Name = "btnDiner";
            btnDiner.Size = new Size(109, 37);
            btnDiner.TabIndex = 1;
            btnDiner.Text = "Diner";
            btnDiner.UseVisualStyleBackColor = true;
            // 
            // btnDrinks
            // 
            btnDrinks.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnDrinks.Location = new Point(390, 96);
            btnDrinks.Name = "btnDrinks";
            btnDrinks.Size = new Size(109, 37);
            btnDrinks.TabIndex = 2;
            btnDrinks.Text = "Drinks";
            btnDrinks.UseVisualStyleBackColor = true;
            // 
            // ListViewMenu
            // 
            ListViewMenu.BorderStyle = BorderStyle.FixedSingle;
            ListViewMenu.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            ListViewMenu.FullRowSelect = true;
            ListViewMenu.GridLines = true;
            ListViewMenu.Location = new Point(56, 189);
            ListViewMenu.Margin = new Padding(2);
            ListViewMenu.Name = "ListViewMenu";
            ListViewMenu.Size = new Size(729, 385);
            ListViewMenu.TabIndex = 9;
            ListViewMenu.UseCompatibleStateImageBehavior = false;
            // 
            // btnSendOrder
            // 
            btnSendOrder.BackColor = Color.Coral;
            btnSendOrder.BorderRadius = 30;
            btnSendOrder.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnSendOrder.ForeColor = Color.White;
            btnSendOrder.Location = new Point(253, 642);
            btnSendOrder.Margin = new Padding(2);
            btnSendOrder.Name = "btnSendOrder";
            btnSendOrder.Size = new Size(314, 48);
            btnSendOrder.TabIndex = 14;
            btnSendOrder.Text = "Send Order";
            btnSendOrder.UseVisualStyleBackColor = false;
            // 
            // Overview
            // 
            Overview.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            Overview.Location = new Point(605, 93);
            Overview.Name = "Overview";
            Overview.Size = new Size(175, 42);
            Overview.TabIndex = 15;
            Overview.Text = "Overview";
            Overview.UseVisualStyleBackColor = true;
            Overview.Click += Overview_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnDelete.Location = new Point(258, 580);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(105, 36);
            btnDelete.TabIndex = 16;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            btnAdd.Location = new Point(453, 579);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(105, 36);
            btnAdd.TabIndex = 17;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(62, 0);
            label1.Name = "label1";
            label1.Size = new Size(93, 38);
            label1.TabIndex = 18;
            label1.Text = "Menu";
            // 
            // cmbCategory
            // 
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(153, 152);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(181, 28);
            cmbCategory.TabIndex = 19;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(65, 152);
            label2.Name = "label2";
            label2.Size = new Size(79, 23);
            label2.TabIndex = 20;
            label2.Text = "Category";
            // 
            // listViewOrderOrders
            // 
            listViewOrderOrders.BorderStyle = BorderStyle.FixedSingle;
            listViewOrderOrders.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            listViewOrderOrders.FullRowSelect = true;
            listViewOrderOrders.GridLines = true;
            listViewOrderOrders.Location = new Point(55, 152);
            listViewOrderOrders.Margin = new Padding(2);
            listViewOrderOrders.Name = "listViewOrderOrders";
            listViewOrderOrders.Size = new Size(730, 422);
            listViewOrderOrders.TabIndex = 21;
            listViewOrderOrders.UseCompatibleStateImageBehavior = false;
            // 
            // cmbService
            // 
            cmbService.FormattingEnabled = true;
            cmbService.Location = new Point(609, 44);
            cmbService.Name = "cmbService";
            cmbService.Size = new Size(168, 28);
            cmbService.TabIndex = 22;
            // 
            // cmbTableNo
            // 
            cmbTableNo.FormattingEnabled = true;
            cmbTableNo.Location = new Point(164, 46);
            cmbTableNo.Name = "cmbTableNo";
            cmbTableNo.Size = new Size(68, 28);
            cmbTableNo.TabIndex = 23;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(538, 46);
            label3.Name = "label3";
            label3.Size = new Size(68, 23);
            label3.TabIndex = 24;
            label3.Text = "Service";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(65, 48);
            label4.Name = "label4";
            label4.Size = new Size(81, 23);
            label4.TabIndex = 24;
            label4.Text = "Table No";
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(857, 729);
            Controls.Add(listViewOrderOrders);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(cmbTableNo);
            Controls.Add(cmbService);
            Controls.Add(label2);
            Controls.Add(cmbCategory);
            Controls.Add(label1);
            Controls.Add(btnAdd);
            Controls.Add(btnDelete);
            Controls.Add(Overview);
            Controls.Add(btnSendOrder);
            Controls.Add(ListViewMenu);
            Controls.Add(btnDrinks);
            Controls.Add(btnDiner);
            Controls.Add(btnLunch);
            Name = "MenuForm";
            Text = "Menu Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLunch;
        private Button btnDiner;
        private Button btnDrinks;
        private ListView ListViewMenu;
        private RoundedButton btnSendOrder;
        private Button Overview;
        private Button btnDelete;
        private Button btnAdd;
        private Label label1;
        private ComboBox cmbCategory;
        private Label label2;
        private ListView listViewOrderOrders;
        private ComboBox cmbService;
        private ComboBox cmbTableNo;
        private Label label3;
        private Label label4;
    }
}