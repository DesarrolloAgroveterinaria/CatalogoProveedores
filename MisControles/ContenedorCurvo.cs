using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MisControles
{
    public class ContenedorCurvo : Component
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,
                int nTopRect,
                int nRightRect,
                int nBottomRect,
                int nWidthEllipse,
                int nHeightEllipse
            );
        private Control _control;
        private int _CornerRadius = 30;
        public Control TargetControl
        {
            get { return _control; }
            set
            {
                _control = value;
                _control.SizeChanged += (sender, eventArgs) => _control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, _control.Width, _control.Height, _CornerRadius, _CornerRadius));
            }
        }

        public int CornerRaiuds
        {
            get { return _CornerRadius; }
            set
            {
                _CornerRadius = value;
                if (_control != null)
                    _control.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, _control.Width, _control.Height, _CornerRadius, _CornerRadius));
            }
        }
    }
}
