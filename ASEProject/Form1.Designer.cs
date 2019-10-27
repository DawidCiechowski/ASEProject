namespace ASEProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBoxCommands = new System.Windows.Forms.RichTextBox();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.pbMainDraw = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainDraw)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxCommands
            // 
            this.richTextBoxCommands.Location = new System.Drawing.Point(13, 13);
            this.richTextBoxCommands.Name = "richTextBoxCommands";
            this.richTextBoxCommands.Size = new System.Drawing.Size(607, 433);
            this.richTextBoxCommands.TabIndex = 0;
            this.richTextBoxCommands.Text = "";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(13, 485);
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(607, 22);
            this.textBoxCommand.TabIndex = 1;
            this.textBoxCommand.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxCommand_KeyUp);
            // 
            // pbMainDraw
            // 
            this.pbMainDraw.Location = new System.Drawing.Point(650, 13);
            this.pbMainDraw.Name = "pbMainDraw";
            this.pbMainDraw.Size = new System.Drawing.Size(630, 493);
            this.pbMainDraw.TabIndex = 2;
            this.pbMainDraw.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 555);
            this.Controls.Add(this.pbMainDraw);
            this.Controls.Add(this.textBoxCommand);
            this.Controls.Add(this.richTextBoxCommands);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbMainDraw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxCommands;
        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.PictureBox pbMainDraw;
    }
}

