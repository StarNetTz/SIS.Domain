using NUnit.Framework;
using SIS.PL.Commands;
using SIS.PL.Events;
using System;

namespace SIS.BDD
{
    class RenameCompetitionTests : CompetitionServiceSpec
    {
        [Test]
        public void can_rename_existing_competition()
        {
            Guid id = Guid.NewGuid();
            Given(new CompetitionAdded() { Id = id, Name = "England Premiership" });
            When(new RenameCompetition() { Id = id, Name = "England Premiership Modified" });
            Expect(new CompetitionRenamed() { Id = id, Name = "England Premiership Modified" });
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void cannot_rename_competition_if_new_name_is_null_or_empty_or_whitespace(string name)
        {
            Guid id = Guid.NewGuid();
            Given(new CompetitionAdded() { Id = id, Name = "England Premiership" });
            When(new RenameCompetition() { Id = id, Name = name });
            ExpectError("InvalidCompetitionName");
        }

        [Test]
        public void cannot_rename_existing_competition_if_another_competition_with_same_name_exist()
        {
            Guid id = Guid.NewGuid();
            Given(new CompetitionAdded() { Id = id, Name = "England Premiership" });
            When(new RenameCompetition() { Id = id, Name = "IExist" });
            ExpectError("DuplicateCompetitionName");
        }
    }
}
