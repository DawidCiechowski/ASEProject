﻿using System;
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
                if (textBoxCommand.Text.ToString().Equals("run()"))
                {
                    string[] commands = richTextBoxCommands.Text.ToString().Split('\n');

                    foreach (string command in commands)
                    {
                        Debug.WriteLine(command);
                    }
                } else if(textBoxCommand.Text.ToString().Equals("clear()"))
                {
                    richTextBoxCommands.Clear();
                }

                textBoxCommand.Clear();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            g.DrawEllipse(new Pen(Color.Red), 100, 100, 30, 30);
            g.Dispose();
        }
    }
}
