using Funq;
using NServiceBus;
using ServiceStack;
using SIS.Api.ServiceInterface;
using SIS.ReadModel;

namespace SIS.Api
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost()
            : base("SIS.Api", typeof(CompetitionServices).Assembly)
        {

        }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            container.RegisterAutoWiredAs<MessageBus, IMessageBus>();
            container.RegisterAutoWiredAs<CompetitionReadModel, ICompetitionReadModel>();
            //Config examples
            //this.Plugins.Add(new PostmanFeature());
            //this.Plugins.Add(new CorsFeature());
        }
    }

    public class MessageBus : IMessageBus
    {
        public void Send(object message)
        {
            ServiceBus.Bus.Send(message);
        }
    }

    public static class ServiceBus
    {
        public static ISendOnlyBus Bus { get; private set; }
        private static readonly object Padlock = new object();
        public static void Init()
        {
            if (Bus != null)
                return;
            lock (Padlock)
            {
                if (Bus != null)
                    return;
                var cfg = new BusConfiguration();
                cfg.UseTransport<MsmqTransport>();
                cfg.UsePersistence<InMemoryPersistence>();
                cfg.EndpointName("SIS.Api");
                cfg.PurgeOnStartup(true);
                cfg.EnableInstallers();

                cfg.Conventions().DefiningCommandsAs(t =>
                {
                    if ((t.Namespace != null) && (t.Namespace.Contains("SIS.PL.Commands")))
                    {
                        return true;
                    }
                    return false;
                });
                Bus = NServiceBus.Bus.CreateSendOnly(cfg);
            }
        }
    }
}