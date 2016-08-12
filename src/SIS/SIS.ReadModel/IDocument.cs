using System;
using System.Linq;

namespace SIS.ReadModel
{
    public interface IDocument
    {
        Guid Id { get; set; }
    }
}