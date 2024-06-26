﻿namespace ChapeauUI
{
    partial class ChapeauUI
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
            panel2 = new Panel();
            buttonWaiter = new RoundedButton();
            buttonChef = new RoundedButton();
            buttonManager = new RoundedButton();
            roundedButton1 = new RoundedButton();
            roundedButton3 = new RoundedButton();
            roundedButton4 = new RoundedButton();
            label1 = new Label();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(buttonWaiter);
            panel2.Controls.Add(buttonChef);
            panel2.Controls.Add(buttonManager);
            panel2.Controls.Add(roundedButton1);
            panel2.Location = new Point(1, -7);
            panel2.Name = "panel2";
            panel2.Size = new Size(205, 1075);
            panel2.TabIndex = 8;
            // 
            // buttonWaiter
            // 
            buttonWaiter.BackColor = Color.Coral;
            buttonWaiter.BorderRadius = 30;
            buttonWaiter.ForeColor = Color.White;
            buttonWaiter.Location = new Point(18, 782);
            buttonWaiter.Name = "buttonWaiter";
            buttonWaiter.Size = new Size(169, 52);
            buttonWaiter.TabIndex = 7;
            buttonWaiter.Text = "Waiter";
            buttonWaiter.UseVisualStyleBackColor = false;
            // 
            // buttonChef
            // 
            buttonChef.BackColor = Color.Coral;
            buttonChef.BorderRadius = 30;
            buttonChef.ForeColor = Color.White;
            buttonChef.Location = new Point(17, 714);
            buttonChef.Name = "buttonChef";
            buttonChef.Size = new Size(169, 52);
            buttonChef.TabIndex = 6;
            buttonChef.Text = "Bar/Kitchen";
            buttonChef.UseVisualStyleBackColor = false;
            // 
            // buttonManager
            // 
            buttonManager.BackColor = Color.Coral;
            buttonManager.BorderRadius = 30;
            buttonManager.ForeColor = Color.White;
            buttonManager.Location = new Point(18, 644);
            buttonManager.Name = "buttonManager";
            buttonManager.Size = new Size(169, 52);
            buttonManager.TabIndex = 5;
            buttonManager.Text = "Manager";
            buttonManager.UseVisualStyleBackColor = false;
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
            roundedButton1.Click += roundedButton1_Click;
            // 
            // roundedButton3
            // 
            roundedButton3.BackColor = Color.Coral;
            roundedButton3.BorderRadius = 30;
            roundedButton3.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            roundedButton3.ForeColor = Color.White;
            roundedButton3.Location = new Point(974, 514);
            roundedButton3.Name = "roundedButton3";
            roundedButton3.Size = new Size(534, 314);
            roundedButton3.TabIndex = 2;
            roundedButton3.Text = "View Orders";
            roundedButton3.UseVisualStyleBackColor = false;
            roundedButton3.Click += roundedButton3_Click;
            // 
            // roundedButton4
            // 
            roundedButton4.BackColor = Color.Coral;
            roundedButton4.BorderRadius = 30;
            roundedButton4.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            roundedButton4.ForeColor = Color.White;
            roundedButton4.Location = new Point(310, 515);
            roundedButton4.Name = "roundedButton4";
            roundedButton4.Size = new Size(534, 314);
            roundedButton4.TabIndex = 1;
            roundedButton4.Text = "View Tables";
            roundedButton4.UseVisualStyleBackColor = false;
            roundedButton4.Click += roundedButton4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(684, 144);
            label1.Name = "label1";
            label1.Size = new Size(507, 65);
            label1.TabIndex = 9;
            label1.Text = "Welcome to Chapeau";
            // 
            // ChapeauUI
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1795, 1058);
            Controls.Add(label1);
            Controls.Add(roundedButton3);
            Controls.Add(panel2);
            Controls.Add(roundedButton4);
            Name = "ChapeauUI";
            Text = "Form1";
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel2;
        private RoundedButton roundedButton1;
        private RoundedButton roundedButton3;
        private RoundedButton roundedButton4;
        private Label label1;
        private RoundedButton buttonWaiter;
        private RoundedButton buttonChef;
        private RoundedButton buttonManager;
    }
}