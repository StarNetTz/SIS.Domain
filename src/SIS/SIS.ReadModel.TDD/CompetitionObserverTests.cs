using NUnit.Framework;
using SIS.PL;
using System;

namespace SIS.ReadModel.TDD
{
    [TestFixture]
    class CompetitionObserverTests
    {
        CompetitionObserver Observer;
        InMemoryRepository<Competition> Repository;

        [SetUp]
        public void Setup()
        {
            Repository = new InMemoryRepository<Competition>();
            var projector = new CompetitionProjector(Repository);
            Observer = new CompetitionObserver(projector);

        }

        [Test]
        public void when_competition_added_event_appears_view_is_created()
        {
            var competitionId = Guid.NewGuid();
            Observer.EventAppeared(new CompetitionAdded() { Id = competitionId, Name = "England Premier" });
            AssertViewCreated(competitionId);
        }
            private void AssertViewCreated(Guid competitionId)
            {
                var cmpt = Repository.GetById(competitionId);
                Assert.That(cmpt.Id == competitionId);
                Assert.That(cmpt.Name == "England Premier");
            }

        [Test]
        public void when_competition_renamed_event_appears_projection_is_updated()
        {
            var competitionId = Guid.NewGuid();
            Observer.EventAppeared(new CompetitionAdded() { Id = competitionId, Name = "England Premier" });
            Observer.EventAppeared(new CompetitionRenamed() { Id = competitionId, Name = "England Premier Updated" });
            AssertViewUpdated(competitionId);
        }
            private void AssertViewUpdated(Guid competitionId)
            {
                var cmpt = Repository.GetById(competitionId);
                Assert.That(cmpt.Id == competitionId);
                Assert.That(cmpt.Name == "England Premier Updated");
            }

    }
}
