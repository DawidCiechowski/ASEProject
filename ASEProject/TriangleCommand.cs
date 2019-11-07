using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ASEProject
{
    class TriangleCommand : Command
    {
        private Form1 f;
        private int side1, side2, side3;

        public TriangleCommand(Form1 f, int side1, int side2, int side3)
        {
            this.f = f;
            this.side1 = side1;
            this.side2 = side2;
            this.side3 = side3;
        }

        public void doAction()
        {
            if (trianglePossible()) 
            {
                
                using (Graphics g = Graphics.FromImage(f.getBitmap()))
                {
                    g.DrawPolygon(Pens.White, drawPoints());
                    g.Dispose();
                }

                f.getMainDraw().Invalidate();
            } else
            {
                System.Windows.Forms.MessageBoxButtons button = System.Windows.Forms.MessageBoxButtons.OK;
                string caption = "Invalid parameters";
                string message = ("Cannot make triangle from these sides");
                System.Windows.Forms.MessageBoxIcon icon = System.Windows.Forms.MessageBoxIcon.Error;
                System.Windows.Forms.MessageBox.Show(message, caption, button, icon);
            }
        }

        public bool trianglePossible()
        {
            if(side1 + side2 > side3 && side1 + side3 > side2 && side2 + side3 > side1)
            {
                return true;
            }

            return false;
        }

        public float calculateAngle1()
        {
            return (float) (Math.Acos((Math.Pow(side2, 2) + Math.Pow(side3, 2) - Math.Pow(side1, 2)) / (2 * side2 * side3)) * 180 / Math.PI);
        }

        public float calculateAngle2()
        {
            return (float)(Math.Acos((Math.Pow(side3, 2) + Math.Pow(side1, 2) - Math.Pow(side2, 2)) / (2 * side1 * side3)) * 180 / Math.PI);
        }

        public Point[] drawPoints()
        {
            float angle1 = calculateAngle1();

            float angle2 = calculateAngle2();

            Point first = new Point();
            Point second = new Point();
            Point third = new Point();

            first.X = f.getCurrentX();
            first.Y = f.getCurrentY();

            second.X = (int)(f.getCurrentX() + side2 * Math.Cos(angle1));
            second.Y = (int)(f.getCurrentY() + side2 * Math.Sin(angle1));

            third.X = (int)(f.getCurrentX() + side3 * Math.Cos(angle2));
            third.Y = (int)(f.getCurrentY() + side3 * Math.Sin(angle2));

            Point[] points = new Point[3];
            points[0] = first;
            points[1] = second;
            points[2] = third;

            return points;
        }
    }
}
