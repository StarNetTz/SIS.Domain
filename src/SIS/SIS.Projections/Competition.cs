using SIS.ReadModel;
using System;

namespace SIS.Projections
{
    public class Competitidffdon : IDocument
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public bool Equals(Competition other)
        {
            if (other == null) return false;
            bool equals = (Id == other.Id) && (Name == other.Name);
            return equals;
        }
    }
}
