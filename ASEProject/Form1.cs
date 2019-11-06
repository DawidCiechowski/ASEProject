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
using System.Text.RegularExpressions;


namespace ASEProject
{
    public partial class Form1 : Form
    {

        Bitmap bm;
        private int currentX, currentY;
     
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
            CommandRunner cr = new CommandRunner(this);

            if (e.KeyCode == Keys.Enter)
            {
                string com = textBoxCommand.Text.ToString();
                if (com.Equals("run"))
                {

                    string[] commands = richTextBoxCommands.Text.ToString().Split('\n');
                    
                    foreach (string command in commands)
                    {
                        cr.executeCommand(command);
                    } 
                }
                else
                {
                    cr.executeCommand(textBoxCommand.Text.ToString());
                    
                }
            }
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            labelX.Text = "Current X: " + currentX;
            labelY.Text = "Current Y: " + currentY;

            commandEditortooltip.SetToolTip(this.richTextBoxCommands, "Text Editor, write your program in here.");
            commandTextBoxToolTip.SetToolTip(this.textBoxCommand, "Single command text box. Type 'run' to execute your entire program.");
        }

        public PictureBox getMainDraw()
        {
            return pbMainDraw;
        }

        public Bitmap getBitmap()
        {
            return bm;
        }

        public int getCurrentX()
        {
            return currentX;
        }

        public void setCurrentX(int x)
        {
            currentX = x;
        }

        public int getCurrentY()
        {
            return currentY;
        }

        public void setCurrentY(int y)
        {
            currentY = y;
        }

        public TextBox getCommandsTextBox()
        {
            return textBoxCommand;
        }

        public RichTextBox getCommandsRichTextBox()
        {
            return richTextBoxCommands;
        }

        public Label getLabelX()
        {
            return labelX;
        }

        public Label getLabelY()
        {
            return labelY;
        }

        public void triangle(int a, int b, int c)
        {
            Point one = new Point();
            using (Graphics g = Graphics.FromImage(bm))
            {
                
                g.Dispose();
            }

            pbMainDraw.Invalidate();
        }

        private void richTextBoxCommands_TextChanged(object sender, EventArgs e)
        {
            HighlightKeyWordsRichTextBox(richTextBoxCommands.Text);
        }

        public void HighlightKeyWordsRichTextBox(string text)
        {
            string expressions = "(circle|rectangle|triangle|clear|moveTo|drawTo)";
            Regex regex = new Regex(expressions);
            MatchCollection mc = regex.Matches(text);
            int startCursorPosition = richTextBoxCommands.SelectionStart;

            foreach(Match m in mc)
            {
                int startIndex = m.Index;
                int stopIndex = m.Length;
                richTextBoxCommands.Select(startIndex, stopIndex);
                richTextBoxCommands.SelectionColor = Color.Green;
                richTextBoxCommands.SelectionStart = startCursorPosition;


                richTextBoxCommands.SelectionColor = Color.FromArgb(0, 104, 184, 236);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadFile load = new LoadFile(this);
            load.load();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFile save = new SaveFile(this);
            save.save();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
