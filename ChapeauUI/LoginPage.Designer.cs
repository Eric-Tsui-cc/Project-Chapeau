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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginPage));
            LoginButton = new RoundedButton();
            wrongUCLabel = new Label();
            userCodeTextBox = new TextBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // LoginButton
            // 
            LoginButton.BackColor = Color.Coral;
            LoginButton.BorderRadius = 30;
            LoginButton.Cursor = Cursors.Hand;
            LoginButton.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point);
            LoginButton.ForeColor = SystemColors.ButtonHighlight;
            LoginButton.Location = new Point(388, 456);
            LoginButton.Margin = new Padding(2, 2, 2, 2);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(467, 66);
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
            wrongUCLabel.Location = new Point(543, 430);
            wrongUCLabel.Margin = new Padding(2, 0, 2, 0);
            wrongUCLabel.Name = "wrongUCLabel";
            wrongUCLabel.Size = new Size(162, 24);
            wrongUCLabel.TabIndex = 2;
            wrongUCLabel.Text = "Wrong User Code";
            wrongUCLabel.Visible = false;
            // 
            // userCodeTextBox
            // 
            userCodeTextBox.BorderStyle = BorderStyle.None;
            userCodeTextBox.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point);
            userCodeTextBox.Location = new Point(388, 378);
            userCodeTextBox.Margin = new Padding(2, 2, 2, 2);
            userCodeTextBox.MaxLength = 4;
            userCodeTextBox.Name = "userCodeTextBox";
            userCodeTextBox.PlaceholderText = "Enter User Code";
            userCodeTextBox.Size = new Size(467, 43);
            userCodeTextBox.TabIndex = 3;
            userCodeTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(439, 102);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(401, 207);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1267, 711);
            Controls.Add(pictureBox1);
            Controls.Add(userCodeTextBox);
            Controls.Add(wrongUCLabel);
            Controls.Add(LoginButton);
            Margin = new Padding(2, 2, 2, 2);
            Name = "LoginPage";
            Text = "LoginPage";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RoundedButton LoginButton;
        private Label wrongUCLabel;
        private TextBox userCodeTextBox;
        private PictureBox pictureBox1;
    }
}