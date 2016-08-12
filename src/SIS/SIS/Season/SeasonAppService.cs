using SIS.PL;
using SIS.PL.Commands;
using StarNet.DDD.Persistence;
using StarNet.DDD.PL;
using System;

namespace SIS
{
    public class SeasonAppService
    {
        readonly IAggregateRepository AggRepository;


        public SeasonAppService(IAggregateRepository repository)
        {
            AggRepository = repository;

        }

        void ChangeAgg(Guid id, Action<SeasonAggregate> usingThisMethod)
        {
            var agg = AggRepository.GetById<SeasonAggregate>(id);
            usingThisMethod(agg);
            AggRepository.Store(agg);
        }

        public void Execute(ICommand command)
        {
            When((dynamic)command);
        }

        private void When(AddSeason c)
        {
            ChangeAgg(c.Id, agg => agg.AddSeason(c));
        }

        private void When(AddStage c)
        {
            ChangeAgg(c.Id, agg => agg.AddStage(c));
        }
    }
}
