using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MisControles
{
    public class PanelDegradado : Panel
    {
        public Color ColorArriba { get; set; }
        public Color ColorAbajo { get; set; }
        public float AnguloDegradado { get; set; }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, this.ColorArriba, this.ColorAbajo, AnguloDegradado);
            Graphics g = e.Graphics;
            g.FillRectangle(lgb, this.ClientRectangle);
            base.OnPaint(e);
        }

    }
}
