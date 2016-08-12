using SIS.PL;
using SIS.PL.Commands;
using StarNet.DDD.Persistence;
using StarNet.DDD.PL;
using System;

namespace SIS
{
    public class MatchAppService
    {
        readonly IAggregateRepository AggRepository;


        public MatchAppService(IAggregateRepository repository)
        {
            AggRepository = repository;

        }

        void ChangeAgg(Guid id, Action<MatchAggregate> usingThisMethod)
        {
            var agg = AggRepository.GetById<MatchAggregate>(id);
            usingThisMethod(agg);
            AggRepository.Store(agg);
        }

        public void Execute(ICommand command)
        {
            When((dynamic)command);
        }

        private void When(ScheduleMatch c)
        {
            ChangeAgg(c.Id, agg => agg.ScheduleMatch(c));
        }
    }
}
