using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEProject
{
    class ClearCommand : Command
    {
        Form1 f;
        public ClearCommand(Form1 f)
        {
            this.f = f;
        }
        public void doAction()
        {
            using(Graphics g = Graphics.FromImage(f.getBitmap()))
            {
                g.Clear(Color.FromArgb(0, 34, 36, 39));
                g.Dispose();
            }

            f.getMainDraw().Invalidate();
        }
    }
}
