using System;
using NUnit.Framework;
using SIS.Api.ServiceInterface;
using SIS.Api.ServiceModel;
using ServiceStack.Testing;
using ServiceStack;
using SIS.ReadModel;

namespace SIS.Api.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private readonly ServiceStackHost appHost;
        class MsgBus : IMessageBus
        {
            public void Send(object message)
            {
                
            }
        }
        public UnitTests()
        {
            appHost = new BasicAppHost(typeof(CompetitionServices).Assembly)
            {
                ConfigureContainer = container =>
                {
                    container.RegisterAutoWiredAs<MsgBus, IMessageBus>();
                    container.RegisterAutoWiredAs<CompetitionReadModel, ICompetitionReadModel>();
                }
            }
            .Init();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }

        [Test]
        public void TestMethod1()
        {
            var service = appHost.Container.Resolve<CompetitionServices>();
            
            var response = service.Any(new FindCompetitions() { SearchString = "test", CurrentPage =0, PageSize = 10  });
            
            Assert.That(response != null);
        }
    }
}
