using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace ASEProject
{
    class Circle : Command
    {
        private Form1 f;
        private int radius;
        public Circle(Form1 f, int radius)
        {
            this.f = f;
            this.radius = radius;
        }
        public void doAction()
        {
            using (Graphics g = Graphics.FromImage(f.getBitmap()))
            {
                g.DrawEllipse(Pens.White, f.getCurrentX(), f.getCurrentY(), radius, radius);
                g.Dispose();
            }

            f.getMainDraw().Invalidate();
        }
    }
}
