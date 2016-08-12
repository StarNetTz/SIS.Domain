using System;
using SIS.PL.Commands;
using SIS.PL.Events;
using StarNet.DDD;

namespace SIS
{
    public class TeamAggregate : Aggregate
    {
        private TeamAggregateState TeamState;

        public TeamAggregate(TeamAggregateState state) : base(state)
        {
            TeamState = state;
        }

        public void AddTeam(AddTeam cmd)
        {
            if (TeamState.Version > 0)
                throw DomainError.Named("TeamAlreadyExists", string.Empty);
            var e = new TeamAdded() { Id = cmd.Id, Name = cmd.Name };
            Apply(e);
        }
    }
}
