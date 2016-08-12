using NUnit.Framework;
using SIS.PL.Commands;
using SIS.PL.Events;
using System;

namespace SIS.BDD
{
    class AddSeasonTests : SeasonServiceSpec
    {
        [Test]
        public void can_add_competition_season_to_archive()
        {
            Guid id = Guid.NewGuid();
            Guid competitionId = Guid.NewGuid();
            Given();
            When(new AddSeason() { Id = id, CompetitionId = competitionId, Name = "2016/2017" });
            Expect(new SeasonAdded() { Id = id, CompetitionId = competitionId, Name = "2016/2017" });
        }

        [Test]
        public void can_add_stage_to_season()
        {
            Guid seasonId = Guid.NewGuid();
            Guid competitionId = Guid.NewGuid();
            Guid stageId = Guid.NewGuid();
            Given(new SeasonAdded() { Id = seasonId, CompetitionId = competitionId, Name = "2016/2017" });
            When(new AddStage() { Id = stageId,SeasonId = seasonId, Name = "Regular season" });
            Expect(new StageAdded() { Id = stageId, SeasonId = seasonId, Name = "Regular season" });
        }
    }
}
