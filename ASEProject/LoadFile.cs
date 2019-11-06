using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ASEProject
{
    class LoadFile
    {
        private Form1 f;

        public LoadFile(Form1 f) 
        {
            this.f = f;
        }

        public void load() 
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            Stream stream;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                if((stream = ofd.OpenFile()) != null)
                {
                    f.getCommandsRichTextBox().Clear();
                    TextReader tr = new StreamReader(stream);
                    f.getCommandsRichTextBox().Text = tr.ReadToEnd();
                    tr.Close();
                    stream.Close();
                }
            }
        }
    }
}
