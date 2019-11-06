﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    class ParameterParser
    {

        public int[] parseParams(string type, string command)
        {
            if (type.Equals("circle"))
            {
                if (validateInput(type, command))
                {
                    int[] parameters = new int[1];
                    parameters[0] = int.Parse(command.Substring(7, command.Length - 8));
                    return parameters;
                } else 
                {
                    invalidArgumentPopUpCircle();
                    return null; 
                }
            }
            else if (type.Equals("move") || type.Equals("draw"))
            {
                if (validateInput(type, command))
                {
                    int[] parameters = new int[2];
                    string[] slicedParams = command.Substring(7, command.Length - 8).Split(',');
                    parameters[0] = int.Parse(slicedParams[0]);
                    parameters[1] = int.Parse(slicedParams[1]);

                    return parameters;
                } else
                {
                    invalidArgumentsPopUp(type);
                }
            } else if(type.Equals("rectangle"))
            {
                if (validateInput(type, command))
                {
                    int[] parameters = new int[2];
                    string[] slicedParams = command.Substring(10, command.Length - 11).Split(',');
                    parameters[0] = int.Parse(slicedParams[0]);
                    parameters[1] = int.Parse(slicedParams[1]);

                    return parameters;
                }
                else
                {
                    invalidArgumentsPopUp(type);
                }
            }

            return null;
        }

        public bool validateInput(string type, string input)
        {
            if (type.Equals("circle"))
            {

                int a;
                return int.TryParse(input.Substring(7, input.Length - 8), out a);
            }
            else if (type.Equals("move") || type.Equals("draw"))
            {
                int a, b;
                string[] slicedParams = input.Substring(7, input.Length - 8).Split(',');
                if (slicedParams.Length != 2)
                {
                    return false;
                }
                else
                {
                    return int.TryParse(slicedParams[0], out a) && int.TryParse(slicedParams[1], out b);
                }
            } else if(type.Equals("rectangle"))
            {
                int a, b;
                string[] slicedParams = input.Substring(10, input.Length - 11).Split(',');
                if (slicedParams.Length != 2)
                {
                    return false;
                }
                else
                {
                    return int.TryParse(slicedParams[0], out a) && int.TryParse(slicedParams[1], out b);
                }
            }
           
            return false;
        }

        public void invalidArgumentsPopUp(string type )
        {
            System.Windows.Forms.MessageBoxButtons button = System.Windows.Forms.MessageBoxButtons.OK;
            string caption = "INVALID PARAMETERs";
            string message = ($"{type} function takes two INTEGER arugments!");
            message = message.Substring(0, 1).ToUpper() + message.Substring(1, message.Length - 1);
            System.Windows.Forms.MessageBoxIcon icon = System.Windows.Forms.MessageBoxIcon.Error;
            System.Windows.Forms.MessageBox.Show(message, caption, button, icon);
        }

        public void invalidArgumentPopUpCircle()
        {
            System.Windows.Forms.MessageBoxButtons button = System.Windows.Forms.MessageBoxButtons.OK;
            string caption = "INVALID PARAMETER";
            string message = "Circle function only takes one Integer arugment";
            System.Windows.Forms.MessageBoxIcon icon = System.Windows.Forms.MessageBoxIcon.Error;
            System.Windows.Forms.MessageBox.Show(message, caption, button, icon);
        }
    }
}