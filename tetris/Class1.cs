using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris_base
{
    public class btn : Button
    {
        public btn()
        {
            this.SetStyle(ControlStyles.Selectable, false);
        }
    }
}
