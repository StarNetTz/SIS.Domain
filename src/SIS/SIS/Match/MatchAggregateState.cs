using SIS.PL.Events;
using StarNet.DDD;
using StarNet.DDD.PL;
using System;

namespace SIS
{
    public class MatchAggregateState : AggregateState
    {
        private Guid StageId { get; set; }
        private int Round { get; set; }
        private DateTime Time { get; set; }
        private Guid HomeTeamId { get; set; }
        private Guid AwayTeamId { get; set; }

        protected override void DelegateWhenToConcreteClass(IEvent ev)
        {
            When((dynamic)ev);
        }

        private void When(MatchScheduled e)
        {
            Id = e.Id;
            StageId = e.StageId;
            Round = e.Round;
            Time = e.Time;
            HomeTeamId = e.HomeTeamId;
            AwayTeamId = e.AwayTeamId;
        }
    }
}
