﻿namespace ChapeauUI
{
    partial class Orders
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
            TableOrdersListView = new ListView();
            panel2 = new Panel();
            roundedButton2 = new RoundedButton();
            roundedButton1 = new RoundedButton();
            roundedButton3 = new RoundedButton();
            roundedButton4 = new RoundedButton();
            comboBoxStatus = new ComboBox();
            roundedButton5 = new RoundedButton();
            btnChangeStatus = new RoundedButton();
            comboBox1 = new ComboBox();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 5;
            // 
            // TableOrdersListView
            // 
            TableOrdersListView.BorderStyle = BorderStyle.FixedSingle;
            TableOrdersListView.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            TableOrdersListView.FullRowSelect = true;
            TableOrdersListView.GridLines = true;
            TableOrdersListView.Location = new Point(251, 42);
            TableOrdersListView.Name = "TableOrdersListView";
            TableOrdersListView.Size = new Size(1363, 599);
            TableOrdersListView.TabIndex = 8;
            TableOrdersListView.UseCompatibleStateImageBehavior = false;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(roundedButton2);
            panel2.Controls.Add(roundedButton1);
            panel2.Controls.Add(roundedButton3);
            panel2.Controls.Add(roundedButton4);
            panel2.Location = new Point(-5, -20);
            panel2.Name = "panel2";
            panel2.Size = new Size(205, 1075);
            panel2.TabIndex = 9;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.Coral;
            roundedButton2.BorderRadius = 30;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(18, 651);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(169, 52);
            roundedButton2.TabIndex = 5;
            roundedButton2.Text = "Home";
            roundedButton2.UseVisualStyleBackColor = false;
            roundedButton2.Click += roundedButton2_Click_1;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.Coral;
            roundedButton1.BorderRadius = 30;
            roundedButton1.ForeColor = Color.White;
            roundedButton1.Location = new Point(18, 933);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.Size = new Size(169, 52);
            roundedButton1.TabIndex = 4;
            roundedButton1.Text = "Log Out";
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += roundedButton1_Click_1;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.Coral;
            roundedButton3.BorderRadius = 30;
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(18, 783);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(169, 52);
            roundedButton3.TabIndex = 2;
            roundedButton3.Text = "Orders";
            roundedButton3.UseVisualStyleBackColor = false;
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.Coral;
            roundedButton4.BorderRadius = 30;
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(18, 716);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(169, 52);
            roundedButton4.TabIndex = 1;
            roundedButton4.Text = "Tables";
            roundedButton4.UseVisualStyleBackColor = false;
            roundedButton4.Click += roundedButton4_Click;
            // 
            // comboBoxStatus
            // 
            comboBoxStatus.Items.AddRange(new object[] { "Ready to Serve", "Preparing", "Longest Waiting Time", "Table ID Ascending", "All" });
            comboBoxStatus.Location = new Point(251, 697);
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(589, 45);
            comboBoxStatus.TabIndex = 10;
            // 
            // roundedButton5
            // 
            roundedButton5.BackColor = Color.Coral;
            roundedButton5.BorderRadius = 30;
            roundedButton5.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(251, 784);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(589, 88);
            roundedButton5.TabIndex = 11;
            roundedButton5.Text = "Sort";
            roundedButton5.UseVisualStyleBackColor = false;
            roundedButton5.Click += roundedButton5_Click;
            // 
            // btnChangeStatus
            // 
            btnChangeStatus.BackColor = Color.Coral;
            btnChangeStatus.BorderRadius = 30;
            btnChangeStatus.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnChangeStatus.ForeColor = Color.White;
            btnChangeStatus.Location = new Point(1025, 784);
            btnChangeStatus.Name = "btnChangeStatus";
            btnChangeStatus.Size = new Size(589, 88);
            btnChangeStatus.TabIndex = 13;
            btnChangeStatus.Text = "Change Table Status";
            btnChangeStatus.UseVisualStyleBackColor = false;
            btnChangeStatus.Click += btnChangeStatus_Click;
            // 
            // comboBox1
            // 
            comboBox1.Items.AddRange(new object[] { "Done", "Preparing", "Served", "Undefined" });
            comboBox1.Location = new Point(1025, 701);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(589, 45);
            comboBox1.TabIndex = 12;
            // 
            // Orders
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1795, 1058);
            Controls.Add(btnChangeStatus);
            Controls.Add(comboBox1);
            Controls.Add(roundedButton5);
            Controls.Add(comboBoxStatus);
            Controls.Add(panel2);
            Controls.Add(TableOrdersListView);
            Controls.Add(label1);
            Name = "Orders";
            Text = "Table Overview";
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Label label1;
        private ListView TableOrdersListView;
        private Panel panel2;
        private RoundedButton roundedButton1;
        private RoundedButton roundedButton3;
        private RoundedButton roundedButton4;
        private RoundedButton roundedButton2;
        private ComboBox comboBoxStatus;
        private RoundedButton roundedButton5;
        private RoundedButton btnChangeStatus;
        private ComboBox comboBox1;
    }
}


