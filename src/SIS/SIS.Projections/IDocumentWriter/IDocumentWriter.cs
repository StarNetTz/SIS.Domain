using SIS.ReadModel;
using System;

namespace SIS.Projections
{
    public interface IDocumentWriter<T> where T : IDocument
    {
        void Add(T document);
        void Update(Guid id, Action<T> usingThisMethod);
    }

    public interface IDocumentReader<T> where T : IDocument
    {
        T Get(Guid id);
    }
}
