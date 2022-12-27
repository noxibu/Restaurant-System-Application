using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_System_Application.Services
{
    internal class DirectoryGenerator
    {
        public DirectoryGenerator()
        {

        }

        public string GetCurrentDirectory()
        {
            var enviroment = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.Parent.FullName;

            return projectDirectory;
        }
    }
}
