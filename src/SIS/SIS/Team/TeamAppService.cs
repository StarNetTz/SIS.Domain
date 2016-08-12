using SIS.PL;
using SIS.PL.Commands;
using StarNet.DDD.Persistence;
using StarNet.DDD.PL;
using System;

namespace SIS
{
    public class TeamAppService
    {
        readonly IAggregateRepository AggRepository;


        public TeamAppService(IAggregateRepository repository)
        {
            AggRepository = repository;

        }

        void ChangeAgg(Guid id, Action<TeamAggregate> usingThisMethod)
        {
            var agg = AggRepository.GetById<TeamAggregate>(id);
            usingThisMethod(agg);
            AggRepository.Store(agg);
        }

        public void Execute(ICommand command)
        {
            When((dynamic)command);
        }

        private void When(AddTeam c)
        {
            ChangeAgg(c.Id, agg => agg.AddTeam(c));
        }
    }
}
