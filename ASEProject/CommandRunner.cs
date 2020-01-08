using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;

namespace ASEProject
{
    class CommandRunner
    {
        //Vars and constructor
        Form1 f;
        RichTextBox commandsTextBox;
        public bool functionFlag, loopFlag, ifFlag, syntaxError;
        Dictionary<string, string> variables = new Dictionary<string, string>();
        List<UserDefinedFunction> userFunctions = new List<UserDefinedFunction>();
        bool currentIfClause = false;

        public CommandRunner(Form1 f)
        {
            this.f = f;
            this.functionFlag = false;
            this.loopFlag = false;
            this.ifFlag = false;
            this.syntaxError = false;
        }

        public CommandRunner(Form1 f, RichTextBox commandsTextBox)
        {
            this.f = f;
            this.commandsTextBox = commandsTextBox;
        }

        /**
         * Create new instance of ParameterParser
         * Get the command, and validate its' type
         * With returned parameters, run any given command.
         */
        
       
        public void executeCommand(string com)
        {
            
            

            if (!functionFlag && !loopFlag && !ifFlag && !syntaxError)
            {
                doTask(com);
            } else
            {
                if(ifFlag)
                {
                    if(currentIfClause)
                    {
                        Debug.WriteLine("TRUE");
                        if (!com.Equals("endif"))
                        {
                            doTask(com);
                            Debug.WriteLine("TASK");
                        } else
                        {
                            ifFlag = false;
                            Debug.WriteLine("FLAG FALSE");
                        }
                    }
                    else
                    {

                        if (com.Equals("endif"))
                        {
                            ifFlag = false;
                        }
                    }
                }
            }
        }

        private bool checkIfClause(string com)
        {
            var val1 = com.Split()[1];
            var val2 = com.Split()[3];

            if(checkIfInVariables(val1)) {
                val1 = variables[val1];
            }

            if (checkIfInVariables(val2))
            {
                val2 = variables[val2];
            }

            return val1.Equals(val2);

        }


        private bool checkIfInVariables(string var)
        {
            return variables.ContainsKey(var);
        }
        private void doTask(string com)
        {
            //Create regular expressions
            string userDefinedRegex = @"^(func)";
            string predefinedFunctionsRegex = @"^(circle|rectangle|triangle|moveTo|drawTo)";
            string predefinedClearingRegex = @"^(clear|clearText)";
            string varRegex = @"^(var)";
            string ifRegex = @"^(if)";
            ParameterParser parser = new ParameterParser();

            if (Regex.IsMatch(com, predefinedFunctionsRegex) && com.EndsWith(")"))
            {
                int[] parameters = parser.parseParams(com, variables);
                if (parameters != null)
                {
                    callAppropriatePredefinedFunction(Regex.Match(com, predefinedFunctionsRegex).ToString(), parameters);
                }
            }
            else if (Regex.IsMatch(com, userDefinedRegex))
            {
                string c = com.Split(new string[] { "func" }, StringSplitOptions.None)[1];
                c = c.Substring(1, c.Length - 1);
            }
            else if (Regex.IsMatch(com, predefinedClearingRegex))
            {
                callAppropriatePredefinedFunction(Regex.Match(com, predefinedClearingRegex).ToString());
            }
            else if (Regex.IsMatch(com, varRegex))
            {
                varClause(com);
            }
            else if (Regex.IsMatch(com, ifRegex))
            {
                ifClause(com);
            }
        }

        private void ifClause(string com)
        {
            if (appropriateIfAssignment(com))
            {
                ifFlag = true;
                currentIfClause = checkIfClause(com);
            }
        }

        private void varClause(string com)
        {
            if (appropriateVarAssignment(com))
            {
                string varName = com.Split()[1];
                string varValue = com.Split()[3];
                addVariable(varName, varValue);
            }
        }

        private bool appropriateIfAssignment(string com)
        {
            string[] parts = com.Split();

            if(parts.Length == 4)
            {
                if (parts[2].Equals("="))
                {
                    return true;
                }
            }

            return false;
        }
        private bool appropriateVarAssignment(string assignment)
        {
            string[] assignmentParts = assignment.Split();

            if(assignmentParts.Length != 4)
            {
                return false;
            } else
            {
                if(assignmentParts[2].Equals("=") && int.TryParse(assignmentParts[3], out int res))
                {
                    return true;
                }
            }

            return false;
        }

        private void addVariable(string varName, string varValue)
        {
            if (!variables.ContainsKey(varName))
            {
                variables.Add(varName, varValue);
            }
        }


        public void callAppropriatePredefinedFunction(string predefinedFunction)
        {
            switch(predefinedFunction)
            {
                case "clear":
                    clearFunction();
                    break;
                case "clearText":
                    clearTextFunction();
                    break;
                default:
                    invalidFunctionPopUp(predefinedFunction);
                    break;
            }
        }

        public void callAppropriatePredefinedFunction(string predefinedFunction, int[] parameters)
        {
            switch (predefinedFunction)
            {
                case "triangle":
                    triangleFunction(parameters);
                    break;
                case "circle":
                    circleFunction(parameters);
                    break;
                case "rectangle":
                    rectangleFunction(parameters);
                    break;
                case "moveTo":
                    moveToFunction(parameters);
                    break;
                case "drawTo":
                    drawToFunction(parameters);
                    break;
                default:
                    invalidFunctionPopUp(predefinedFunction);
                    break;
            }
        }

        public void circleFunction(int[] parameters)
        {
            Circle circle = new Circle(f, parameters[0]);
            circle.doAction();
        }

        public void rectangleFunction(int[] parameters)
        {
            RectangleCommand rect = new RectangleCommand(f, parameters[0], parameters[1]);
            rect.doAction();
        }

        public void triangleFunction(int[] parameters)
        {
            TriangleCommand triangle = new TriangleCommand(f, parameters[0], parameters[1], parameters[2]);
            triangle.doAction();
        }

        public void drawToFunction(int[] parameters)
        {
            DrawToCommand drawTo = new DrawToCommand(f, parameters[0], parameters[1]);
            drawTo.doAction();
        }

        public void moveToFunction(int[] parameters)
        {
            MoveToCommand moveTo = new MoveToCommand(f, parameters[0], parameters[1]);
            moveTo.doAction();
        }

        public void clearFunction()
        {
            ClearCommand cc = new ClearCommand(f);
            cc.doAction();
        }

        public void clearTextFunction()
        {
            f.getCommandsRichTextBox().Clear();
        }

        public void invalidFunctionPopUp(string type)
        {
            
            System.Windows.Forms.MessageBoxButtons button = System.Windows.Forms.MessageBoxButtons.OK;
            string caption = "INVALID FUNCTION NAME";
            string message = ($"{type} function doesn't exist in the current context!");
            message = message.Substring(0, 1).ToUpper() + message.Substring(1, message.Length - 1);
            System.Windows.Forms.MessageBoxIcon icon = System.Windows.Forms.MessageBoxIcon.Error;
            System.Windows.Forms.MessageBox.Show(message, caption, button, icon);
        }

        public void syntaxErrorPopUp(string com)
        {

            System.Windows.Forms.MessageBoxButtons button = System.Windows.Forms.MessageBoxButtons.OK;
            string caption = "SYNTAX ERROR";
            string message = ($"Syntax error at: {com}");
            System.Windows.Forms.MessageBoxIcon icon = System.Windows.Forms.MessageBoxIcon.Error;
            System.Windows.Forms.MessageBox.Show(message, caption, button, icon);
        }

        public void clearAll()
        {
            variables.Clear();
            userFunctions.Clear();
            ifFlag = false;
            loopFlag = false;
            functionFlag = false;
            currentIfClause = false;
        }
    }
}
