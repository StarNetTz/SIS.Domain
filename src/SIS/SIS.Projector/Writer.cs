using System;

namespace SIS.Projector
{
    // will be automatically wired up by default convention
    public class Writer : IWriter
    {
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
