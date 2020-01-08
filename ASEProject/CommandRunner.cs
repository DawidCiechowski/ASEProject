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
        Dictionary<string,UserDefinedFunction> userFunctions = new Dictionary<string, UserDefinedFunction>();
        bool currentIfClause = false;
        bool functionExecutionFlag = false;
        string currentForLoop = "";
        string loop = "";
        List<string> forLoopCommands = new List<string>();
        string currentFunction = "";

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

                if(functionFlag)
                {
                    if(!com.Equals("endfunc"))
                    {
                        Debug.WriteLine("I'm here");
                        userFunctions[currentFunction].addTask(com);
                    } else
                    {
                        functionFlag = false;
                    }
                }
                if(ifFlag)
                {
                    executeIfClauseBehaviour(com);
                }

                if (loopFlag)
                {
                    if (!com.Equals("endfor"))
                    {
                        forLoopCommands.Add(com);
                    }
                    else
                    {
                        executeForLoop();
                    }
                }

            }
        }

        private void executeForLoop()
        {
            if(currentForLoop.Equals("Basic"))
            {
                executeBasicForLoop();
            } else if(currentForLoop.Equals("Ranged"))
            {
                executeRangedForLoop();
            } else
            {
                executeSteppedForLoop();
            }
        }

        private void executeBasicForLoop()
        {
            int iterations = getIterations();
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < forLoopCommands.Count(); j++)
                {
                    doTask(forLoopCommands[j]);
                }
            }
        }

        private void executeRangedForLoop()
        {
            int iterations = getIterations();
            int startIndex = getStartIndex();


            for (int i = startIndex; i < iterations; i++)
            {
                for (int j = 0; j < forLoopCommands.Count(); j++)
                {
                    doTask(forLoopCommands[j]);
                }
            }
        }

        private void executeSteppedForLoop()
        {
            int iterations = getIterations();
            int startIndex = getStartIndex();
            int step = getStep();

            for (int i = startIndex; i < iterations; i += step)
            {
                for (int j = 0; j < forLoopCommands.Count(); j++)
                {
                    doTask(forLoopCommands[j]);
                }
            }
        }

        private int getStartIndex()
        {
            string var = loop.Split()[5].Split(',')[0];
            if (checkIfInVariables(var))
            {
                return int.Parse(variables[var]);
            }
            else
            {
                return int.Parse(var);
            }
        }

        private int getStep()
        {
            string var = loop.Split()[5].Split(',')[2];
            if (checkIfInVariables(var))
            {
                return int.Parse(variables[var]);
            }
            else
            {
                return int.Parse(var);
            }
        }

        private int getIterations()
        {
            string[] vars = loop.Split()[5].Split(',');
            if (vars.Length == 1)
            {
                if (checkIfInVariables(vars[0]))
                {
                    return int.Parse(variables[vars[0]]);
                }
                else
                {
                    return int.Parse(vars[0]);
                }
            } else
            {
                if (checkIfInVariables(vars[1]))
                {
                    return int.Parse(variables[vars[1]]);
                }
                else
                {
                    return int.Parse(vars[1]);
                }
            }

         
        }
        private void executeIfClauseBehaviour(string com)
        {
            if (currentIfClause)
            {
                if (!com.Equals("endif"))
                {
                    doTask(com);
                }
                else
                {
                    ifFlag = false;
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
            string forRegex = @"^(for)";
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
                
                userFunction(com);

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
            } else if(Regex.IsMatch(com, forRegex))
            {
                forLoop(com);
            } else if(variables.ContainsKey(com.Split()[0]))
            {
                reassignValue(com);
            } if(userFunctions.ContainsKey(com.Split('(')[0]))
            {
                currentFunction = com.Split('(')[0];
                functionExecutionFlag = true;
                executeFunction();
                //executeCommand(com);
            }
        }

        private void executeFunction()
        {
            if (validCall())
            {
                for (int i = 0; i < userFunctions[currentFunction].getTasks().Count(); i++)
                {
                    doTask(userFunctions[currentFunction].getTasks().ElementAt(i));
                }
                functionExecutionFlag = false;
            }
        }
        private bool validCall()
        {
            return true;
        }

        private void userFunction(string com)
        {
         
            string[] func = com.Split(new string[] { "func" }, StringSplitOptions.None);
            string function = func[1];
            if (validFunctionAssignment(func, function))
            {
                createUserFunction(function);
            } else
            {
                functionFlag = false;
            }
        }

        private void createUserFunction(string function)
        {
            
            string[] functionParts = function.Split('(');
            string functionName = functionParts[0].Substring(1, functionParts[0].Length-1);
            if(functionParts[1].Length > 1)
            {
                string[] functionParams = functionParts[1].Substring(0, functionParts[1].Length - 2).Split(',');
                userFunctions.Add(functionName, new UserDefinedFunction(functionName, functionParams.Length));
                currentFunction = functionName;
                functionFlag = true;
            } else
            {
                userFunctions.Add(functionName, new UserDefinedFunction(functionName));
                currentFunction = functionName;
                functionFlag = true;
            }   
            
        }

        private bool validFunctionAssignment(string[] func, string function)
        {
            if(func.Length == 2)
            {
                if(function.Contains("(") && function.EndsWith(")"))
                {
                    if(function.Count(f => f == '(') == 1 && function.Count(f => f == ')') == 1)
                    {
                        if(!function.StartsWith("(") || !function.StartsWith(")"))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private void reassignValue(string com)
        {
            if(validReassignment(com))
            {
                int reassignedValue = int.Parse(variables[com.Split()[0]]) + int.Parse(com.Split()[2]);
                variables[com.Split()[0]] = reassignedValue.ToString();
            }
        }

        private bool validReassignment(string com)
        {
            if(com.Split()[1].Equals("+") && int.TryParse(com.Split()[2], out int re))
            {
                return true;
            }

            return false;
        }

        private void forLoop(string com)
        {
            if(appropriateForLoopAssignment(com))
            {
                loopFlag = true;
            }
        }

        private bool appropriateForLoopAssignment(string com)
        {
            //for var i in range = 10
            //for var i in range = 1,10
            //for var i in range = 1,10,3
            string[] parts = com.Split();
            if (parts.Length == 6)
            {
                if (parts[5].Split(',').Length == 1 && int.TryParse(parts[5], out int q))
                {
                    currentForLoop = "Basic";
                    loop = com;
                    return basicForLoop(com, parts);
                } else if(parts[5].Split(',').Length == 2)
                {

                    string[] forLoopNumbers = parts[5].Split(',');
                    string start = forLoopNumbers[0];
                    string range = forLoopNumbers[1];
                    if (int.TryParse(start, out int s) && int.TryParse(range, out int r))
                    {
                        loop = com;
                        currentForLoop = "Ranged";
                        return rangedForLoop(com, parts);
                    }
                } else if(parts[5].Split(',').Length == 3)
                {
                    string[] forLoopNumbers = parts[5].Split(',');
                    string start = forLoopNumbers[0];
                    string range = forLoopNumbers[1];
                    string step = forLoopNumbers[2];

                    if (int.TryParse(start, out int s) && int.TryParse(range, out int r) && int.TryParse(step, out int st))
                    {
                        loop = com;
                        currentForLoop = "Step";
                        return stepForLoop(com, parts);
                    }
                } else
                {
                    syntaxErrorPopUp(com);
                }
            } 
            else
            {
                syntaxErrorPopUp(com);
            }

            return false;
        }

        private bool basicForLoop(string com, string[] parts)
        {
            if (parts[4].Equals("="))
            {
                    if (int.TryParse(parts[5], out int res))
                    {
                        return true;
                    }
                    else
                    {
                        syntaxErrorPopUp(com);
                    }

            }
            else
            {
                syntaxErrorPopUp(com);
                return false;
            }

            return false;
        }

        private bool rangedForLoop(string com, string[] parts)
        {
            if (parts[4].Equals("="))
            {
                if (int.TryParse(parts[5].Split(',')[0], out int res) && int.TryParse(parts[5].Split(',')[1], out int r))
                {
                    return true;
                }
                else
                {
                    syntaxErrorPopUp(com);
                }

            }
            else
            {
                syntaxErrorPopUp(com);
            }

            return false;
        }

        private bool stepForLoop(string com, string[] parts)
        {
            if (parts[4].Equals("="))
            {
                if (int.TryParse(parts[5].Split(',')[0], out int res) && int.TryParse(parts[5].Split(',')[1], out int r)
                    && int.TryParse(parts[5].Split(',')[2], out int s))
                {
                    return true;
                }
                else
                {
                    syntaxErrorPopUp(com);
                }

            }
            else
            {
                syntaxErrorPopUp(com);
            }

            return false;
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
            currentForLoop = "";
            loop = "";
            forLoopCommands.Clear();
            functionExecutionFlag = false;
        }
    }
}
