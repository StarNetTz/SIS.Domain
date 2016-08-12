using System;
using SIS.PL.Commands;
using SIS.PL.Events;
using StarNet.DDD;

namespace SIS
{
    public class MatchAggregate : Aggregate
    {
        private MatchAggregateState MatchState;

        public MatchAggregate(MatchAggregateState state) : base(state)
        {
            MatchState = state;
        }

        public void ScheduleMatch(ScheduleMatch cmd)
        {
            var e = new MatchScheduled() { Id = cmd.Id, StageId = cmd.StageId, Round = cmd.Round, Time = cmd.Time, HomeTeamId = cmd.HomeTeamId ,AwayTeamId = cmd.AwayTeamId };
            Apply(e);
        }
    }
}
