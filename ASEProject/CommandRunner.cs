using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    class CommandRunner
    {
        //Vars and constructor
        Form1 f;
        public CommandRunner(Form1 f)
        {
            this.f = f;
        }

        /**
         * Create new instance of ParameterParser
         * Get the command, and validate its' type
         * With returned parameters, run any given command.
         */
        public void executeCommand(string com)
        {
            ParameterParser parser = new ParameterParser();
            if (com.Equals("clearText"))
            {
                f.getCommandsRichTextBox().Clear();
            }
            else if (com.Equals("clear"))
            {
                ClearCommand cc = new ClearCommand(f);
                cc.doAction();
            }
            else if (com.Contains("circle(") && com.EndsWith(")"))
            {
                int[] radius = parser.parseParams("circle", com);
                if (radius != null)
                {
                    Circle circle = new Circle(f, radius[0]);
                    circle.doAction();
                }
            }
            else if (com.Contains("moveTo(") && com.EndsWith(")"))
            {
                int[] parameters = parser.parseParams("move", com);
                if (parameters != null)
                {
                    MoveToCommand move = new MoveToCommand(f, parameters[0], parameters[1]);
                    move.doAction();
                }
            }
            else if (com.Contains("drawTo(") && com.EndsWith(")"))
            {
                int[] parameters = parser.parseParams("draw", com);
                if (parameters != null)
                {
                    DrawToCommand draw = new DrawToCommand(f, parameters[0], parameters[1]);
                    draw.doAction();
                }
            }
            else if (com.Contains("rectangle(") && com.EndsWith(")"))
            {
                int[] parameters = parser.parseParams("rectangle", com);
                if (parameters != null)
                {
                    RectangleCommand rect = new RectangleCommand(f, parameters[0], parameters[1]);
                    rect.doAction();
                }

            } else if(com.Contains("triangle(") && com.EndsWith(")"))
            {
                int[] parameters = parser.parseParams("triangle", com);
                if (parameters != null)
                {
                    TriangleCommand triangle = new TriangleCommand(f, parameters[0], parameters[1], parameters[2]);
                    triangle.doAction();
                }
            } else
            {
                parser.parseParams("Unkown", com);
            }

            f.getCommandsTextBox().Clear();
        }
    }
}
