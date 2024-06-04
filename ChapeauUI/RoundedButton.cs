using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ChapeauUI
{
    public class RoundedButton : Button
    {
        public int BorderRadius { get; set; } = 30;

        private Color defaultBackColor = Color.Coral;
        private Color hoverBackColor = Color.Tomato;

        private Color defaultForeColor = Color.White;
        private Color hoverForeColor = Color.White;

        public RoundedButton()
        {
            // Set the initial colors to the button's default colors
            this.BackColor = defaultBackColor;
            this.ForeColor = defaultForeColor;

            // Add event handlers for mouse enter and leave
            this.MouseEnter += RoundedButton_MouseEnter;
            this.MouseLeave += RoundedButton_MouseLeave;
        }

        // Event handler for mouse entering the button
        private void RoundedButton_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = hoverBackColor;
            this.ForeColor = hoverForeColor;
        }

        // Event handler for mouse leaving the button
        private void RoundedButton_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = defaultBackColor;
            this.ForeColor = defaultForeColor;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(new Rectangle(0, 0, BorderRadius, BorderRadius), 180, 90);
            path.AddArc(new Rectangle(this.Width - BorderRadius, 0, BorderRadius, BorderRadius), 270, 90);
            path.AddArc(new Rectangle(this.Width - BorderRadius, this.Height - BorderRadius, BorderRadius, BorderRadius), 0, 90);
            path.AddArc(new Rectangle(0, this.Height - BorderRadius, BorderRadius, BorderRadius), 90, 90);
            path.CloseAllFigures();

            this.Region = new Region(path);

            base.OnPaint(pevent);

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            pevent.Graphics.FillPath(new SolidBrush(this.BackColor), path);
            pevent.Graphics.DrawPath(new Pen(this.ForeColor, 1), path);

            TextRenderer.DrawText(pevent.Graphics, this.Text, this.Font, this.ClientRectangle, this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
    }
}
