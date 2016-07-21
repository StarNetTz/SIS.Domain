using SIS.PL;
using StarNet.DDD;
using System;

namespace SIS
{
    public class CompetitionAggregate : Aggregate
    {
        private CompetitionAggregateState CompetitionState;

        public CompetitionAggregate(CompetitionAggregateState state) : base(state)
        {
            CompetitionState = state;
        }

        public void AddCompetition(AddCompetition cmd)
        {
            if (CompetitionState.Version > 0)
                throw new CompetitionAlreadyExistsException();
            var e = new CompetitionAdded() { Id = cmd.Id, Name = cmd.Name };
            Apply(e);
        }

        public void RenameCompetition(RenameCompetition cmd)
        {
           
            var e = new CompetitionRenamed() { Id = cmd.Id, Name = cmd.Name };
            Apply(e);
        }

        public class CompetitionAlreadyExistsException : Exception  {}
    }
}
