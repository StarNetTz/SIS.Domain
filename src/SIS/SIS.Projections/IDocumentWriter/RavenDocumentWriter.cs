using SIS.ReadModel;
using System;

namespace SIS.Projections
{
    public class RavenDocumentWriter<T> : IDocumentWriter<T> where T : IDocument
    {
        public void Add(T document)
        {
            using (var ses = RavenGlobal.DocumentStore.OpenSession())
            {
                ses.Store(document);
                ses.SaveChanges();
            }
        }

        public void Update(Guid id, Action<T> usingThisMethod)
        {
            using (var ses = RavenGlobal.DocumentStore.OpenSession())
            {
                var doc = ses.Load<T>(id);
                usingThisMethod(doc);
                ses.SaveChanges();
            }
        }
    }
}
