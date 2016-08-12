using NUnit.Framework;
using SIS.PL.Commands;
using SIS.PL.Events;
using System;

namespace SIS.BDD
{
    class ScheduleMatchTests : MatchServiceSpec
    {
        [Test]
        public void can_schedule_match()
        {
            Guid id = Guid.NewGuid();
            Guid stageId = Guid.NewGuid();
            DateTime matchTime = new DateTime(2016, 4, 4, 20, 0, 0);
            Guid homeTeamId = Guid.NewGuid();
            Guid awayTeamId = Guid.NewGuid();

            Given();
            When(new ScheduleMatch() { Id = id,  StageId = stageId, Round = 1, Time = matchTime, HomeTeamId = homeTeamId, AwayTeamId = awayTeamId });
            Expect(new MatchScheduled() { Id = id, StageId = stageId, Round = 1, Time = matchTime, HomeTeamId = homeTeamId, AwayTeamId = awayTeamId });
        }
    }
}
