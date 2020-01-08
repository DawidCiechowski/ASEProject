using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject
{
    class UserDefinedFunction : Command
    {
        private int numOfParams;
        private string name;
        private Form1 f;
        private Dictionary<string, object> parameters;
        private List<string> tasks;
        


        public UserDefinedFunction(string name, int numOfParams, Form1 f, Dictionary<string, object> parameters)
        {
            this.name = name;
            this.numOfParams = numOfParams;
            this.f = f;
            this.parameters = parameters;
            tasks = new List<string>();
        }

        public void doAction()
        {
            if (verifyFunctionCall())
            {
                CommandRunner cr = new CommandRunner(f);
                for (int i = 0; i < tasks.Count(); ++i)
                {
                    cr.executeCommand(tasks.ElementAt(i));
                }
            }
        }

        public void addTask(string task)
        {
            tasks.Add(task);
        }

        public bool verifyFunctionCall()
        {
            return false;
        }

    }
}
