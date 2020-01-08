using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEProject
{
    class DrawToCommand : Command
    {
        private Form1 f;
        private int x, y;

        public DrawToCommand(Form1 f, int x, int y)
        {
            this.f = f;
            this.x = x;
            this.y = y;
        }
        public void doAction()
        {
            using (Graphics g = Graphics.FromImage(f.getBitmap()))
            {
                Point currentPoint = new Point(f.getCurrentX(), f.getCurrentY());
                Point drawToPoint = new Point(x, y);
                g.DrawLine(new Pen(Color.White), currentPoint, drawToPoint);
                g.Dispose();
            } 

            f.getMainDraw().Invalidate();
            f.setCurrentX(x);
            f.setCurrentY(y);
            f.getLabelX().Text = "Current X: " + x;
            f.getLabelY().Text = "Current Y: " + y;
        }
    }
}
