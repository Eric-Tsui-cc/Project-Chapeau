namespace ChapeauUI
{
    partial class KitchenViewPage
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
            label1 = new Label();
            listView1 = new ListView();
            OrderId = new ColumnHeader();
            ItemName = new ColumnHeader();
            Quantity = new ColumnHeader();
            Table = new ColumnHeader();
            Comment = new ColumnHeader();
            Time = new ColumnHeader();
            Status = new ColumnHeader();
            listViewFinishOrders = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            Switch = new Button();
            PrepareButton = new Button();
            ReadyButton = new Button();
            BackButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(39, 26);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(215, 54);
            label1.TabIndex = 0;
            label1.Text = "OrderView";
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { OrderId, ItemName, Quantity, Table, Comment, Time, Status });
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
            listView1.Location = new Point(39, 94);
            listView1.Name = "listView1";
            listView1.Size = new Size(1026, 478);
            listView1.TabIndex = 1;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // OrderId
            // 
            OrderId.Text = "id";
            OrderId.Width = 50;
            // 
            // ItemName
            // 
            ItemName.Text = "Item Name";
            ItemName.Width = 120;
            // 
            // Quantity
            // 
            Quantity.Text = "Qty";
            Quantity.Width = 50;
            // 
            // Table
            // 
            Table.Text = "Table ";
            Table.Width = 50;
            // 
            // Comment
            // 
            Comment.Text = "Comment";
            Comment.Width = 180;
            // 
            // Time
            // 
            Time.Text = "Wait Time";
            Time.Width = 100;
            // 
            // Status
            // 
            Status.Text = "Status";
            Status.Width = 100;
            // 
            // listViewFinishOrders
            // 
            listViewFinishOrders.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader5, columnHeader6, columnHeader7 });
            listViewFinishOrders.FullRowSelect = true;
            listViewFinishOrders.GridLines = true;
            listViewFinishOrders.Location = new Point(39, 94);
            listViewFinishOrders.Name = "listViewFinishOrders";
            listViewFinishOrders.Size = new Size(1026, 478);
            listViewFinishOrders.TabIndex = 2;
            listViewFinishOrders.UseCompatibleStateImageBehavior = false;
            listViewFinishOrders.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "id";
            columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Item Name";
            columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Qty";
            columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Table ";
            columnHeader4.Width = 50;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Comment";
            columnHeader5.Width = 180;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Wait Time";
            columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Status";
            columnHeader7.Width = 100;
            // 
            // Switch
            // 
            Switch.Location = new Point(287, 28);
            Switch.Name = "Switch";
            Switch.Size = new Size(232, 52);
            Switch.TabIndex = 3;
            Switch.Text = "Check Served order";
            Switch.UseVisualStyleBackColor = true;
            Switch.Click += Switch_Click;
            // 
            // PrepareButton
            // 
            PrepareButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            PrepareButton.Location = new Point(1106, 309);
            PrepareButton.Name = "PrepareButton";
            PrepareButton.Size = new Size(182, 103);
            PrepareButton.TabIndex = 4;
            PrepareButton.Text = "Prepare !";
            PrepareButton.UseVisualStyleBackColor = true;
            // 
            // ReadyButton
            // 
            ReadyButton.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            ReadyButton.Location = new Point(1106, 469);
            ReadyButton.Name = "ReadyButton";
            ReadyButton.Size = new Size(182, 103);
            ReadyButton.TabIndex = 5;
            ReadyButton.Text = "Raady !";
            ReadyButton.UseVisualStyleBackColor = true;
            // 
            // BackButton
            // 
            BackButton.Location = new Point(557, 31);
            BackButton.Name = "BackButton";
            BackButton.Size = new Size(152, 46);
            BackButton.TabIndex = 6;
            BackButton.Text = "Back";
            BackButton.UseVisualStyleBackColor = true;
            BackButton.Click += BackButton_Click;
            // 
            // KitchenViewPage
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1316, 686);
            Controls.Add(BackButton);
            Controls.Add(ReadyButton);
            Controls.Add(PrepareButton);
            Controls.Add(Switch);
            Controls.Add(listViewFinishOrders);
            Controls.Add(listView1);
            Controls.Add(label1);
            Margin = new Padding(2);
            Name = "KitchenViewPage";
            Text = "Tables";
            Load += KitchenViewPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListView listView1;
        private ColumnHeader ItemName;
        private ColumnHeader Quantity;
        private ColumnHeader Table;
        private ColumnHeader Comment;
        private ColumnHeader Time;
        private ColumnHeader Status;
        private ColumnHeader OrderId;
        private ListView listViewFinishOrders;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private Button Switch;
        private Button PrepareButton;
        private Button ReadyButton;
        private Button BackButton;
    }
}