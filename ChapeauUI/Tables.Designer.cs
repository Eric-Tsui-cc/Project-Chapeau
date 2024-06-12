namespace ChapeauUI
{
    partial class Tables
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
            TableOverview = new ListView();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(11, 12);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(134, 54);
            label1.TabIndex = 0;
            label1.Text = "Tables";
            // 
            // TableOverview
            // 
            TableOverview.Location = new Point(11, 68);
            TableOverview.Margin = new Padding(2, 2, 2, 2);
            TableOverview.Name = "TableOverview";
            TableOverview.Size = new Size(1309, 601);
            TableOverview.TabIndex = 1;
            TableOverview.UseCompatibleStateImageBehavior = false;
            // 
            // Tables
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1316, 686);
            Controls.Add(TableOverview);
            Controls.Add(label1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Tables";
            Text = "Tables";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListView TableOverview;
    }
}