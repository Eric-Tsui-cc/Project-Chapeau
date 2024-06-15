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
            ItemName = new ColumnHeader();
            Quantity = new ColumnHeader();
            Table = new ColumnHeader();
            Comment = new ColumnHeader();
            Time = new ColumnHeader();
            Status = new ColumnHeader();
            OrderId = new ColumnHeader();
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
            // ItemName
            // 
            ItemName.Text = "Item Name";
            ItemName.Width = 180;
            // 
            // Quantity
            // 
            Quantity.Text = "Quantity";
            Quantity.Width = 100;
            // 
            // Table
            // 
            Table.Text = "Table ";
            Table.Width = 80;
            // 
            // Comment
            // 
            Comment.Text = "Comment";
            Comment.Width = 300;
            // 
            // Time
            // 
            Time.Text = "Wait Time";
            Time.Width = 150;
            // 
            // Status
            // 
            Status.Text = "Status";
            Status.Width = 120;
            // 
            // OrderId
            // 
            OrderId.Text = "Order Id";
            OrderId.Width = 90;
            // 
            // KitchenViewPage
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1316, 686);
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
    }
}