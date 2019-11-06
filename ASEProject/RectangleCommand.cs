using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ASEProject
{
    class RectangleCommand : Command
    {
        private Form1 f;
        private int width, height;

        public RectangleCommand(Form1 f, int width, int height)
        {
            this.f = f;
            this.width = width;
            this.height = height;
        }
        public void doAction()
        {
            using (Graphics g = Graphics.FromImage(f.getBitmap()))
            {
                g.DrawRectangle(Pens.White, f.getCurrentX(), f.getCurrentY(), width, height);
                g.Dispose();
            }

            f.getMainDraw().Invalidate();
        }
    }
}
