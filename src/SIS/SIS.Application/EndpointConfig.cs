
namespace SIS.Application
{
    using System;
    using NServiceBus;
    using SIS;
    using StarNet.DDD.GetEventStore;
    using EventStore.ClientAPI;
    using System.Configuration;
    using ReadModel;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/the-nservicebus-host
	*/

    class CompetitionReadModel : ICompetitionReadModel
    {
        public Competition FindById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool IsCompetitionNameUnique(Guid id, string name)
        {
            return true;
        }

        public PaginatedResult<Competition> Search(PaginatedQuery request)
        {
            throw new NotImplementedException();
        }

    }
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(BusConfiguration configuration)
        {
            // NServiceBus provides the following durable storage options
            // To use RavenDB, install-package NServiceBus.RavenDB and then use configuration.UsePersistence<RavenDBPersistence>();
            // To use SQLServer, install-package NServiceBus.NHibernate and then use configuration.UsePersistence<NHibernatePersistence>();

            // If you don't need a durable storage you can also use, configuration.UsePersistence<InMemoryPersistence>();
            // more details on persistence can be found here: http://docs.particular.net/nservicebus/persistence-in-nservicebus

            //Also note that you can mix and match storages to fit you specific needs. 
            //http://docs.particular.net/nservicebus/persistence-order
            configuration.RegisterComponents(reg => {
                var cnn = EventStoreConnection.Create(ConfigurationManager.ConnectionStrings["GetEventStore"].ToString());
                cnn.ConnectAsync().Wait();
                reg.RegisterSingleton(typeof(IEventStoreConnection), cnn);
                reg.ConfigureComponent<GetEventStoreRepository>(DependencyLifecycle.SingleInstance);
                reg.ConfigureComponent<CompetitionAppService>(DependencyLifecycle.SingleInstance);
                reg.ConfigureComponent<CompetitionReadModel>(DependencyLifecycle.SingleInstance);

            });
            configuration.UsePersistence<InMemoryPersistence>();
            configuration.Conventions().DefiningCommandsAs(t =>
            {
                if ((t.Namespace != null) && (t.Namespace.Contains("SIS.PL.Commands")))
                {
                    return true;
                }
                return false;
            });
        }
    }
}
