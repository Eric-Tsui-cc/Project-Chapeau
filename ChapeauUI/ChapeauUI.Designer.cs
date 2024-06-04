namespace ChapeauUI
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
            panel1 = new Panel();
            OpenTables = new RoundedButton();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(118, 1004);
            panel1.TabIndex = 0;
            // 
            // OpenTables
            // 
            OpenTables.BackColor = Color.Coral;
            OpenTables.BorderRadius = 30;
            OpenTables.ForeColor = Color.White;
            OpenTables.Location = new Point(1564, 43);
            OpenTables.Name = "OpenTables";
            OpenTables.Size = new Size(169, 52);
            OpenTables.TabIndex = 1;
            OpenTables.Text = "Tables";
            OpenTables.UseVisualStyleBackColor = false;
            // 
            // ChapeauUI
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1795, 1058);
            Controls.Add(OpenTables);
            Controls.Add(panel1);
            Name = "ChapeauUI";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private RoundedButton OpenTables;
    }
}