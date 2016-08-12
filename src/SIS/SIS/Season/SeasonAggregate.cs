using System;
using SIS.PL.Commands;
using SIS.PL.Events;
using StarNet.DDD;

namespace SIS
{
    public class SeasonAggregate : Aggregate
    {
        private SeasonAggregateState SeasonState;

        public SeasonAggregate(SeasonAggregateState state) : base(state)
        {
            SeasonState = state;
        }

        public void AddSeason(AddSeason cmd)
        {
            if (SeasonState.Version > 0)
                throw DomainError.Named("SeasonAlreadyExists", string.Empty);
            var e = new SeasonAdded() { Id = cmd.Id, CompetitionId = cmd.CompetitionId, Name = cmd.Name };
            Apply(e);
        }

        internal void AddStage(AddStage cmd)
        {
            var e = new StageAdded() { Id = cmd.Id, SeasonId = cmd.SeasonId, Name = cmd.Name };
            Apply(e);
        }
    }
}
