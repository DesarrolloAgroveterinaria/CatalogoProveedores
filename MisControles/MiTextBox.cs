using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MisControles
{
    public partial class MiTextBox : TextBox
    {
        private Color _BordeColor;
        private int _BordeAncho;

        public Color BordeColor
        {
            get
            {
                return _BordeColor;
            }

            set
            {
                _BordeColor = value;
                this.Controls[0].BackColor = _BordeColor;
            }
        }

        public int BordeAncho
        {
            get
            {
                return _BordeAncho;
            }

            set
            {
                _BordeAncho = value;
                this.Controls[0].Height = _BordeAncho;
            }
        }

        public MiTextBox()
        {
            InitializeComponent();
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            AutoSize = false; //Allows you to change height to have bottom padding
            Controls.Add(new Label()
            { Height = 1, Dock = DockStyle.Bottom, BackColor = BordeColor });
        }
    }
}
