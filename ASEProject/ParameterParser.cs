using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace ASEProject
{
    public class ParameterParser
    {



        public int[] parseParams(string command)
        {

            MatchCollection matches = checkFunction(command);
            if (matches.Count > 0)
            {
                string funcVars = Regex.Match(matches[0].ToString(), @"\([^)]*\)").ToString();
                string funcType = matches[0].ToString().Split(funcVars.ToCharArray())[0];
                string[] vars = funcVars.Substring(1, funcVars.Length - 2).Split(',');


                if (validateInput(funcType, vars))
                {
                    return returnValues(vars);
                }
                else
                {
                    callAppropriateErrorMessage(funcType);
                }
            }

            return null;
        }

        public MatchCollection checkFunction(string command)
        {
            string regularExpression = @"circle\([^)]*\)|triangle\([^)]*\)|rectangle\([^)]*\)|moveTo\([^)]*\)|drawTo\([^)]*\)";
            Regex r = new Regex(regularExpression);
            return r.Matches(command);
        }

        public int[] returnValues(string[] vars)
        {
            int[] returnVars = new int[vars.Length];
            for (int i = 0; i < vars.Length; ++i)
            {
                returnVars[i] = int.Parse(vars[i].ToString());
            }

            return returnVars;
        }

        public void callAppropriateErrorMessage(string funcType)
        {
            if (funcType.Equals("circle"))
            {
                invalidArgumentsPopUp(funcType, 1);
            }
            else if (funcType.Equals("rectangle") || funcType.Equals("moveTo") || funcType.Equals("drawTo"))
            {
                invalidArgumentsPopUp(funcType, 2);
            }
            else if (funcType.Equals("triangle"))
            {
                invalidArgumentsPopUp(funcType, 3);
            }
        }

        /**
         * @params type - what type of command is the program supposed to check
         * @params input - the actual command written by the user
         * Check the type of command
         * split the command written by the user, to check what's inbetween the brackets - seperated by a comma.
         * if the number of arguments is correct, check if they are parsable, to ensure the type is ok.
         * if everything is fine return true, else false
         */
        public bool validateInput(string type, string[] vars)
        {
       
            if(type.Equals("circle"))
            {
                return checkOneParam(vars);
            } else if(type.Equals("rectangle") || type.Equals("moveTo") || type.Equals("drawTo"))
            {
                return checkTwoParams(vars);
            } else if(type.Equals("triangle"))
            {
                return checkThreeParams(vars);
            }
           
            return false;
        }


        //Check parameters functions
        public bool checkOneParam(string[] param)
        {
            Debug.WriteLine(param.Length);

            if (param.Length != 1)
            {
                return false;
            }
            else
            {
                int result;
                return int.TryParse(param[0], out result);
            }
        }

        public bool checkTwoParams(string[] parameters)
        {
            if (parameters.Length != 2)
            {
                return false;
            }
            else
            {
                int result;
                return (int.TryParse(parameters[0], out result) && int.TryParse(parameters[1], out result));
            }
        }

        public bool checkThreeParams(string[] parameters)
        {
            if (parameters.Length != 3)
            {
                return false;
            }
            else
            {
                int result;
                return int.TryParse(parameters[0], out result) && int.TryParse(parameters[1], out result) && int.TryParse(parameters[2], out result);
            }
        }

        //Invalid arguments pop-up message.
        public void invalidArgumentsPopUp(string type, int numberOfArgs)
        {
            string args;

            if(numberOfArgs > 1)
            {
                args = "arguments";
            } else
            {
                args = "argument";
            }
            System.Windows.Forms.MessageBoxButtons button = System.Windows.Forms.MessageBoxButtons.OK;
            string caption = "INVALID PARAMETERs";
            string message = ($"{type} function takes {numberOfArgs} INTEGER {args}!");
            message = message.Substring(0, 1).ToUpper() + message.Substring(1, message.Length - 1);
            System.Windows.Forms.MessageBoxIcon icon = System.Windows.Forms.MessageBoxIcon.Error;
            System.Windows.Forms.MessageBox.Show(message, caption, button, icon);
        }
    }
}
