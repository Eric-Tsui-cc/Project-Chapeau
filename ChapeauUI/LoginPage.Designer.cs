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
            LoginButton.Location = new Point(282, 380);
            LoginButton.Margin = new Padding(2, 2, 2, 2);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(340, 55);
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
            wrongUCLabel.Location = new Point(395, 358);
            wrongUCLabel.Margin = new Padding(2, 0, 2, 0);
            wrongUCLabel.Name = "wrongUCLabel";
            wrongUCLabel.Size = new Size(126, 20);
            wrongUCLabel.TabIndex = 2;
            wrongUCLabel.Text = "Wrong User Code";
            wrongUCLabel.Visible = false;
            // 
            // userCodeTextBox
            // 
            userCodeTextBox.BorderStyle = BorderStyle.None;
            userCodeTextBox.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point);
            userCodeTextBox.Location = new Point(282, 315);
            userCodeTextBox.Margin = new Padding(2, 2, 2, 2);
            userCodeTextBox.MaxLength = 4;
            userCodeTextBox.Name = "userCodeTextBox";
            userCodeTextBox.PlaceholderText = "Enter User Code";
            userCodeTextBox.Size = new Size(340, 36);
            userCodeTextBox.TabIndex = 3;
            userCodeTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(931, 570);
            Controls.Add(userCodeTextBox);
            Controls.Add(wrongUCLabel);
            Controls.Add(LoginButton);
            Margin = new Padding(2, 2, 2, 2);
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