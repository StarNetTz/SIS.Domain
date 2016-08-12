using System;
using System.Linq;

namespace SIS.ReadModel
{
    public class Competition : IDocument
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}