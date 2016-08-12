using SIS.ReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Projections.Tests
{
    public class InMemoryDocumentRepository<T> : IDocumentWriter<T>, IDocumentReader<T> where T : IDocument
    {
        Dictionary<Guid, T> Store = new Dictionary<Guid, T>();

        public void Add(T document)
        {
            Store.Add(document.Id, document);
        }

        public T Get(Guid id)
        {
            if (Store.ContainsKey(id))
                return Store[id];
            return default(T);
        }

        public void Update(Guid id, Action<T> usingThisMethod)
        {
            if (!Store.ContainsKey(id))
                throw new ApplicationException("Document does not exist in store!");
            var doc = Store[id];
            usingThisMethod(doc);
            Store[id] = doc;
        }
    }
}
