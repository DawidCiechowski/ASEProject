using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    class MoveToCommand : Command
    {
        Form1 f;
        private int x, y;

        public MoveToCommand(Form1 f, int x, int y)
        {
            this.f = f;
            this.x = x;
            this.y = y;
        }

        public void doAction()
        {
            f.setCurrentX(x);
            f.setCurrentY(y);

            f.getLabelX().Text = "Current X: " + x;
            f.getLabelY().Text = "Current Y: " + y;
        }
    }
}
