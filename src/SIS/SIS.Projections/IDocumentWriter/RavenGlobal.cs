using Raven.Client;
using Raven.Client.Document;
using System;

namespace SIS.Projections
{
    public static class RavenGlobal
    {

        private static readonly Lazy<IDocumentStore> theDocStore = new Lazy<IDocumentStore>(() =>
        {
            var docStore = new DocumentStore
            {
                ConnectionStringName = "RavenDB"
            };
            docStore.Initialize();
            docStore.DatabaseCommands.GlobalAdmin.EnsureDatabaseExists(docStore.DefaultDatabase);
            docStore.Conventions.SaveEnumsAsIntegers = true;

            //OPTIONAL:
            //IndexCreation.CreateIndexes(typeof(Global).Assembly, docStore);

            return docStore;
        });

        public static IDocumentStore DocumentStore
        {
            get { return theDocStore.Value; }
        }
    }
}
