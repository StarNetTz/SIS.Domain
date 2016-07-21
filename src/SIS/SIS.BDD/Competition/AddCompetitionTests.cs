using NUnit.Framework;
using SIS.PL;
using System;
using static SIS.CompetitionAggregate;

namespace SIS.BDD
{
    class AddCompetitionTests : CompetitionServiceSpec
    {
        [Test]
        public void can_add_competition_to_archive()
        {
            Guid id = Guid.NewGuid();
            Given();
            When(new AddCompetition() { Id = id, Name = "England Premiership" });
            Expect(new CompetitionAdded() { Id = id, Name = "England Premiership" });
        }

        [Test]
        public void cannot_add_competition_to_archive_if_competition_with_same_id_already_exists()
        {
            Guid id = Guid.NewGuid();
            Given(new CompetitionAdded() { Id = id, Name = "England Premiership" });
            When(new AddCompetition() { Id = id, Name = "England Premiership" });
            ExpectException<CompetitionAlreadyExistsException>();
        }
    }
}
