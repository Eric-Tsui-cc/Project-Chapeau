using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace ChapeauUI
{
    public class RoundedButton : Button
    {
        //SETS BORDER RADIUS TO 30
        public int BorderRadius { get; set; } = 30;

        //DEFINE BUTTON COLORS AND HOVER COLORS
        private Color defaultBackColor = Color.Coral;
        private Color hoverBackColor = Color.Tomato;

        private Color defaultForeColor = Color.White;
        private Color hoverForeColor = Color.White;

        public RoundedButton()
        {
            // Set the initial colors to the button's default colors
            this.BackColor = defaultBackColor;
            this.ForeColor = defaultForeColor;

            //   //The += operator is used to attach the event handler methods to the events.
            this.MouseEnter += RoundedButton_MouseEnter;
            this.MouseLeave += RoundedButton_MouseLeave;
        }
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

        /*The OnPaint method is overridden to customize the drawing of the button. It is responsible for rendering
 the button with rounded corners,filling it with the background color, drawing the border, and rendering
 the text centered on the button.*/
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