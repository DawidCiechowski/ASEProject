using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ASEProject
{
    public partial class Form1 : Form
    {

        Bitmap bm;
        int currentX;
        int currentY;
        public Form1()
        {
            InitializeComponent();
            //Initialize bitmap
            bm = new Bitmap(pbMainDraw.Size.Width, pbMainDraw.Size.Height);
            pbMainDraw.Image = bm;

            //Initialize coordinates

            currentX = 0;
            currentY = 0;
        }

        private void textBoxCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string com = textBoxCommand.Text.ToString();
                if (com.Equals("run"))
                {

                    string[] commands = richTextBoxCommands.Text.ToString().Split('\n');

                    foreach (string command in commands)
                    {
                        executeCommand(command);
                    } 
                }
                else
                {
                    executeCommand(textBoxCommand.Text.ToString());
                }
            }
        }

        public void executeCommand(string com)
        {
            ParameterParser parser = new ParameterParser();
            if (com.Equals("clearText"))
            {
                richTextBoxCommands.Clear();
            }
            else if (com.Equals("clear"))
            {
                clearCommand();
            }
            else if (com.Contains("circle(") && com.EndsWith(")"))
            {
                int[] radius = parser.parseParams("circle", com);
                if (radius != null)
                {
                    drawCircle(radius[0]);
                }
            }
            else if (com.Contains("moveTo(") && com.EndsWith(")"))
            {
                int[] parameters = parser.parseParams("move", com);
                if (parameters != null)
                {
                    moveTo(parameters[0], parameters[1]);
                }
            }
            else if (com.Equals("currentX"))
            {
                Debug.WriteLine(currentX);
            } else if(com.Contains("drawTo(") && com.EndsWith(")"))
            {
                int[] parameters = parser.parseParams("draw", com);
                if (parameters != null)
                {
                    drawTo(parameters[0], parameters[1]);
                }
            } else if(com.Contains("rectangle(") && com.EndsWith(")"))
            {
                int[] parameters = parser.parseParams("rectangle", com);
                    if(parameters != null)
                {
                    rect(parameters[0], parameters[1]);
                }
                
            }

            textBoxCommand.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Make image background white on Load
            clearCommand();
        }

        public void clearCommand()
        {
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.Clear(Color.White);
                g.Dispose();
            }

            pbMainDraw.Invalidate();
        }

        public void drawCircle(int radius)
        {
            using(Graphics g = Graphics.FromImage(bm))
            {
                g.DrawEllipse(new Pen(Color.Red), currentX, currentY, radius, radius);
                g.Dispose();
            }

            pbMainDraw.Invalidate();
        }

        public void drawTo(int x, int y)
        {
            using(Graphics g = Graphics.FromImage(bm))
            {
                Point currentPoint = new Point(currentX, currentY);
                Point drawToPoint = new Point(x, y);
                g.DrawLine(new Pen(Color.Red), currentPoint, drawToPoint);
                g.Dispose();
            }

            pbMainDraw.Invalidate();

            currentX = x;
            currentY = y;
        } 

        public void moveTo(int x, int y)
        {
            currentX = x;
            currentY = y;
        }

        public void rect(int width, int height)
        {
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.DrawRectangle(new Pen(Color.Red), currentX, currentY, width, height);
                g.Dispose();
            }

            pbMainDraw.Invalidate();
        }
    }
}
