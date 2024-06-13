namespace ChapeauUI
{
    partial class TableInfoFormDetail
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
            comboBox1 = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            label5 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(34, 22);
            label1.Name = "label1";
            label1.Size = new Size(248, 28);
            label1.TabIndex = 0;
            label1.Text = "Orderid(s) of the table";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(320, 22);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(82, 32);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(192, 86);
            label2.Name = "label2";
            label2.Size = new Size(90, 31);
            label2.TabIndex = 2;
            label2.Text = "Status";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(320, 86);
            label3.Name = "label3";
            label3.Size = new Size(0, 31);
            label3.TabIndex = 3;
            // 
            // label4
            // 
            label4.Location = new Point(34, 164);
            label4.Name = "label4";
            label4.Size = new Size(527, 198);
            label4.TabIndex = 4;
            // 
            // button1
            // 
            button1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(445, 22);
            button1.Name = "button1";
            button1.Size = new Size(116, 72);
            button1.TabIndex = 5;
            button1.Text = "Confrim Served";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(34, 139);
            label5.Name = "label5";
            label5.Size = new Size(183, 25);
            label5.TabIndex = 6;
            label5.Text = "Order information:";
            // 
            // TableInfoFormDetail
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(588, 390);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Name = "TableInfoFormDetail";
            Text = "TableInfoFormDetail";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button1;
        private Label label5;
    }
}