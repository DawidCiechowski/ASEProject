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
                        Debug.WriteLine(command);
                    }
                } else if(com.Equals("clearText"))
                {
                    richTextBoxCommands.Clear();
                } else if(com.Equals("clear"))
                {
                    clearCommand();
                } else if(com.Contains("circle(") && com.EndsWith(")"))
                {
                    if(validateInput("circle", com))
                    {
                        drawCircle(int.Parse(com.Substring(7, com.Length - 8)));
                    }
                }

                textBoxCommand.Clear();
            }
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

            string f = "(LL)";
            string[] s = f.Split(); 
        }

        public bool validateInput(string type, string input)
        {
            if(type.Equals("circle"))
            {

                int a;
                return int.TryParse(input.Substring(7, input.Length-8), out a);
            }

            return false;
        }
    }
}
