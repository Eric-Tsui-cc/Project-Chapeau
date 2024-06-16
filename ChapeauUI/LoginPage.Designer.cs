namespace ChapeauUI
{
    partial class LoginPage
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
            LoginButton = new RoundedButton();
            wrongUCLabel = new Label();
            userCodeTextBox = new TextBox();
            SuspendLayout();
            // 
            // LoginButton
            // 
            LoginButton.BackColor = Color.Coral;
            LoginButton.BorderRadius = 30;
            LoginButton.Cursor = Cursors.Hand;
            LoginButton.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point);
            LoginButton.ForeColor = SystemColors.ButtonHighlight;
            LoginButton.Location = new Point(529, 703);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(637, 102);
            LoginButton.TabIndex = 1;
            LoginButton.Text = "Log In";
            LoginButton.UseVisualStyleBackColor = false;
            LoginButton.Click += LoginButton_Click;
            // 
            // wrongUCLabel
            // 
            wrongUCLabel.AutoSize = true;
            wrongUCLabel.BackColor = Color.Transparent;
            wrongUCLabel.ForeColor = Color.IndianRed;
            wrongUCLabel.Location = new Point(740, 663);
            wrongUCLabel.Name = "wrongUCLabel";
            wrongUCLabel.Size = new Size(228, 37);
            wrongUCLabel.TabIndex = 2;
            wrongUCLabel.Text = "Wrong User Code";
            wrongUCLabel.Visible = false;
            // 
            // userCodeTextBox
            // 
            userCodeTextBox.BorderStyle = BorderStyle.None;
            userCodeTextBox.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point);
            userCodeTextBox.HideSelection = false;
            userCodeTextBox.Location = new Point(529, 583);
            userCodeTextBox.MaxLength = 4;
            userCodeTextBox.Name = "userCodeTextBox";
            userCodeTextBox.PlaceholderText = "Enter User Code";
            userCodeTextBox.Size = new Size(637, 64);
            userCodeTextBox.TabIndex = 3;
            userCodeTextBox.TextAlign = HorizontalAlignment.Center;
            userCodeTextBox.TextChanged += userCodeTextBox_TextChanged;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1728, 1096);
            Controls.Add(userCodeTextBox);
            Controls.Add(wrongUCLabel);
            Controls.Add(LoginButton);
            Name = "LoginPage";
            Text = "LoginPage";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundedButton LoginButton;
        private Label wrongUCLabel;
        private TextBox userCodeTextBox;
    }
}