﻿namespace ChapeauUI
{
    partial class TableInfoForm
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
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(52, 77);
            button1.Name = "button1";
            button1.Size = new Size(166, 72);
            button1.TabIndex = 0;
            button1.Text = "Change Table Status";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(293, 77);
            button2.Name = "button2";
            button2.Size = new Size(163, 72);
            button2.TabIndex = 1;
            button2.Text = "Check Details";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // TableInfoForm
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(514, 255);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "TableInfoForm";
            Text = "TableInfoForm";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
    }
}