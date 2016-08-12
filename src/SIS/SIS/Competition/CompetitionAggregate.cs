using SIS.PL.Commands;
using SIS.PL.Events;
using StarNet.DDD;

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
                throw DomainError.Named("CompetitionAlreadyExists", string.Empty);
            var e = new CompetitionAdded() { Id = cmd.Id, Name = cmd.Name };
            Apply(e);
        }

        public void RenameCompetition(RenameCompetition cmd)
        {
            var e = new CompetitionRenamed() { Id = cmd.Id, Name = cmd.Name };
            Apply(e);
        }
    }
}
