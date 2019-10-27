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
                if (validateInput("circle", com))
                {
                    drawCircle(parseParams("circle", com)[0]);
                }
            }
            else if (com.Contains("moveTo(") && com.EndsWith(")"))
            {
                if (validateInput("move", com))
                {
                    int[] parameters = parseParams("move", com);
                    moveTo(parameters[0], parameters[1]);
                }
            }
            else if (com.Equals("currentX"))
            {
                Debug.WriteLine(currentX);
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

        public bool validateInput(string type, string input)
        {
            if(type.Equals("circle"))
            {

                int a;
                return int.TryParse(input.Substring(7, input.Length-8), out a);
            } else if(type.Equals("move"))
            {
                int a, b;
                string[] slicedParams = input.Substring(7, input.Length - 8).Split(',');
                if(slicedParams.Length != 2)
                {
                    return false;
                } else
                {
                    return int.TryParse(slicedParams[0], out a) && int.TryParse(slicedParams[1], out b);
                }
            }

            return false;
        }

        public int[] parseParams(string type, string command)
        {
            if(type.Equals("circle"))
            {
                int[] parameters = new int[1];
                parameters[0] = int.Parse(command.Substring(7, command.Length - 8));
                return parameters;
            } else if(type.Equals("move"))
            {
                int[] parameters = new int[2];
                string[] slicedParams = command.Substring(7, command.Length - 8).Split(',');
                parameters[0] = int.Parse(slicedParams[0]);
                parameters[1] = int.Parse(slicedParams[1]);
                Debug.WriteLine(slicedParams[0]+ " " + slicedParams[1]);

                return parameters;
            }

            return null;
        }

        public void moveTo(int x, int y)
        {
            currentX = x;
            currentY = y;
        }
    }
}
