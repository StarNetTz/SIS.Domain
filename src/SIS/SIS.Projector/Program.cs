using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace SIS.Projector
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Container.For<ConsoleRegistry>();

            var app = container.GetInstance<Application>();
            app.Run();
            Console.ReadLine();
        }
    }
}
