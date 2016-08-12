using NUnit.Framework;
using SIS.PL.Commands;
using SIS.PL.Events;
using System;

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
            ExpectError("CompetitionAlreadyExists");
        }


        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void cannot_add_competition_to_archive_if_competition_name_is_null_or_empty_or_whitespace(string name)
        {
            When(new AddCompetition() { Id = Guid.NewGuid(), Name = name });
            ExpectError("InvalidCompetitionName");
        }

        [Test]
        public void cannot_add_competition_to_archive_if_competition_with_same_name_and_different_id_already_exists()
        {
            Guid id = Guid.NewGuid();
            Given(new CompetitionAdded() { Id = id, Name = "IExist" });
            When(new AddCompetition() { Id = Guid.NewGuid(), Name = "IExist" });
            ExpectError("DuplicateCompetitionName");
        }
    }
}
