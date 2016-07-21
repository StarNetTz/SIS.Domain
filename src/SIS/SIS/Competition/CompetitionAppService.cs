using SIS.PL;
using StarNet.DDD.Persistence;
using System;

namespace SIS
{
    public class CompetitionAppService
    {
        readonly IAggregateRepository Repository;
        public CompetitionAppService(IAggregateRepository repository)
        {
            Repository = repository;
        }

        void ChangeAgg(Guid id, Action<CompetitionAggregate> usingThisMethod)
        {
            var agg = Repository.GetById<CompetitionAggregate>(id);
            usingThisMethod(agg);
            Repository.Save(agg);
        }

        public void Execute(ICommand command)
        {
            When((dynamic)command);
        }

        private void When(AddCompetition c)
        {
            ChangeAgg(c.Id, agg => agg.AddCompetition(c));
        }

        private void When(RenameCompetition c)
        {
            ChangeAgg(c.Id, agg => agg.RenameCompetition(c));
        }
    }
}
