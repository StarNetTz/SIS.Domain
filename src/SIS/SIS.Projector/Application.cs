using SIS.Projections;
using System;
using System.Linq;
using System.Reflection;
using Topshelf;

namespace SIS.Projector
{
    public class Application
    {
        private readonly IWriter _writer;
        private readonly ILog _logger;


        public Application(IWriter writer, ILog logger)
        {
            _writer = writer;
            _logger = logger;

        }

        public void Run()
        {

            var util = new GetEventStoreProjectionsSetupUtil();
            util.CreateAggregateProjectionStreamsFor(Assembly.GetAssembly(typeof(CompetitionAggregate)));

            _logger.Info(nameof(Application) + " started.");

            _writer.WriteLine("Hello World!");

            _logger.Info(nameof(Application) + " finished.");

            HostFactory.Run(x =>
            {
                x.Service<ProjectorService>(s =>
                {
                    s.ConstructUsing(name => new ProjectorService());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("SIS Aggregate listener");
                x.SetDisplayName("SIS Aggregate listener");
                x.SetServiceName("SIS Aggregate listener");
            });
        }
    }
}
