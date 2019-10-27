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
        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(pbMainDraw.Size.Width, pbMainDraw.Size.Height);
            pbMainDraw.Image = bm;
        }

        private void textBoxCommand_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (textBoxCommand.Text.ToString().Equals("run"))
                {

                    drawCircle();

                    string[] commands = richTextBoxCommands.Text.ToString().Split('\n');

                    foreach (string command in commands)
                    {
                        Debug.WriteLine(command);
                    }
                } else if(textBoxCommand.Text.ToString().Equals("clearText"))
                {
                    richTextBoxCommands.Clear();
                } else if(textBoxCommand.Text.ToString().Equals("clear"))
                {
                    clearCommand();
                }

                textBoxCommand.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Make image background white on Load
            //clearCommand();
            
            drawCircle();
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

        public void drawCircle()
        {
            using(Graphics g = Graphics.FromImage(bm))
            {
                g.DrawEllipse(new Pen(Color.Red), 100, 100, 30, 30);
                g.Dispose();
            }

            pbMainDraw.Invalidate();
        }
    }
}
