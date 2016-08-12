using NUnit.Framework;
using SIS.PL.Commands;
using SIS.PL.Events;
using System;

namespace SIS.BDD
{
    class AddTeamTests : TeamServiceSpec
    {
        [Test]
        public void can_add_team_to_archive()
        {
            Guid id = Guid.NewGuid();
            Given();
            When(new AddTeam() { Id = id,  Name = "F.K. Sloboda" });
            Expect(new TeamAdded() { Id = id, Name = "F.K. Sloboda" });
        }
    }
}
