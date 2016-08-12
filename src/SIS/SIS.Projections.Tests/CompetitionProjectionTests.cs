using NUnit.Framework;
using SIS.PL.Events;
using SIS.ReadModel;
using System;

namespace SIS.Projections.Tests
{

    [TestFixture]
    public class CompetitionProjectionTests
    {
        TestEventSubscriber Subscriber;
        InMemoryDocumentRepository<Competition> Repository;
        CompetitionProjection Projection;

        [SetUp]
        public void Setup()
        {
            Subscriber = new TestEventSubscriber();
            Repository = new InMemoryDocumentRepository<Competition>();
            Projection = new CompetitionProjection(Subscriber, Repository);
        }

         [Test]
        public void can_project_CompetitionAdded()
        {
            var id = Guid.NewGuid();
            Subscriber.Publish(new CompetitionAdded() { Id = id, Name = "England Premier" });

            var expectedDoc = new Competition() { Id = id, Name = "England Premier" };

            AssertProjection(id, expectedDoc);
        }


        [Test]
        public void can_project_CompetitionRenamed()
        {
            var id = Guid.NewGuid();
            Subscriber.Publish(
                new CompetitionAdded() { Id = id, Name = "England Premier" },
                new CompetitionRenamed() { Id = id, Name = "England Premiership" }
                );

            var expectedDoc = new Competition() { Id = id, Name = "England Premiership" };

            AssertProjection(id, expectedDoc);
        }

        private void AssertProjection(Guid id, Competition expected)
        {
            Assert.That(expected.Equals(Repository.Get(id)));
        }
    }
}
