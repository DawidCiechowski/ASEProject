﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace ASEProject
{
    class SaveFile
    {
        private Form1 f;
        private SaveFileDialog sfd;
        private MemoryStream ms;
        public SaveFile(Form1 f)
        {
            this.f = f;
            ms = new MemoryStream();
        }

        public void save()
        {
            f.getCommandsRichTextBox().SaveFile(ms, RichTextBoxStreamType.PlainText);
            ms.WriteByte(13);
            

            sfd = new SaveFileDialog();
            sfd.CreatePrompt = true;
            sfd.OverwritePrompt = true;

            sfd.FileName = "My Program";
            sfd.DefaultExt = "txt";
            sfd.Filter = "Text (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DialogResult result = sfd.ShowDialog();
            Stream stream;
            if(result == DialogResult.OK)
            {
                stream = sfd.OpenFile();
                ms.Position = 0;
                ms.WriteTo(stream);
                stream.Close();
            }
        }
    }
}
