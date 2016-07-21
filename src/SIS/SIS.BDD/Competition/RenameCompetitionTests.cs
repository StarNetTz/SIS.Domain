using NUnit.Framework;
using SIS.PL;
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
    }
}
