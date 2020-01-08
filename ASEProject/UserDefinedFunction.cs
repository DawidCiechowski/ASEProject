using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ASEProject
{
    class UserDefinedFunction 
    {
        private int numOfParams = 0;
        private string name;
        private List<string> tasks;
        private Dictionary<string, string> parameters;




        public UserDefinedFunction(string name)
        {
            this.name = name;
            parameters = new Dictionary<string, string>();
            tasks = new List<string>();
        }

        public UserDefinedFunction(string name, int  nOOfParameters)
        {
            this.name = name;
            this.numOfParams = nOOfParameters;
            parameters = new Dictionary<string, string>();
            tasks = new List<string>();
        }

        public void addTask(string task)
        {
            tasks.Add(task);
        }

        public List<string> getTasks()
        {
            return tasks;
        }

        public void setParams(string varName, string varValue)
        {
            parameters.Add(varName, varValue);
        }
    }
}
