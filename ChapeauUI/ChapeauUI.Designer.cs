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
            panel2 = new Panel();
            roundedButton2 = new RoundedButton();
            roundedButton5 = new RoundedButton();
            roundedButton6 = new RoundedButton();
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
            panel2.Controls.Add(roundedButton2);
            panel2.Controls.Add(roundedButton5);
            panel2.Controls.Add(roundedButton6);
            panel2.Controls.Add(roundedButton1);
            panel2.Location = new Point(1, -7);
            panel2.Name = "panel2";
            panel2.Size = new Size(205, 1075);
            panel2.TabIndex = 8;
            // 
            // roundedButton2
            // 
            roundedButton2.BackColor = Color.Coral;
            roundedButton2.BorderRadius = 30;
            roundedButton2.ForeColor = Color.White;
            roundedButton2.Location = new Point(18, 643);
            roundedButton2.Name = "roundedButton2";
            roundedButton2.Size = new Size(169, 52);
            roundedButton2.TabIndex = 8;
            roundedButton2.Text = "Home";
            roundedButton2.UseVisualStyleBackColor = false;
            // 
            // roundedButton5
            // 
            roundedButton5.BackColor = Color.Coral;
            roundedButton5.BorderRadius = 30;
            roundedButton5.ForeColor = Color.White;
            roundedButton5.Location = new Point(18, 775);
            roundedButton5.Name = "roundedButton5";
            roundedButton5.Size = new Size(169, 52);
            roundedButton5.TabIndex = 7;
            roundedButton5.Text = "Orders";
            roundedButton5.UseVisualStyleBackColor = false;
            roundedButton5.Click += roundedButton5_Click_1;
            // 
            // roundedButton6
            // 
            roundedButton6.BackColor = Color.Coral;
            roundedButton6.BorderRadius = 30;
            roundedButton6.ForeColor = Color.White;
            roundedButton6.Location = new Point(18, 708);
            roundedButton6.Name = "roundedButton6";
            roundedButton6.Size = new Size(169, 52);
            roundedButton6.TabIndex = 6;
            roundedButton6.Text = "Tables";
            roundedButton6.UseVisualStyleBackColor = false;
            roundedButton6.Click += roundedButton6_Click_1;
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
        private RoundedButton roundedButton2;
        private RoundedButton roundedButton5;
        private RoundedButton roundedButton6;
    }
}