using SIS.PL;
using StarNet.DDD.Persistence;
using System;

namespace SIS
{
    public class CompetitionAppService
    {
        readonly IAggregateRepository AggRepository;
        readonly ICompetitionReadModel ReadModelRepository;

        public CompetitionAppService(IAggregateRepository repository, ICompetitionReadModel readModel)
        {
            AggRepository = repository;
            ReadModelRepository = readModel;
        }

        void ChangeAgg(Guid id, Action<CompetitionAggregate> usingThisMethod)
        {
            var agg = AggRepository.GetById<CompetitionAggregate>(id);
            usingThisMethod(agg);
            AggRepository.Save(agg);
        }

        public void Execute(ICommand command)
        {
            When((dynamic)command);
        }

        private void When(AddCompetition c)
        {
            new AddCompetitionValidator(ReadModelRepository).Validate(c);
            ChangeAgg(c.Id, agg => agg.AddCompetition(c));
        }

        private void When(RenameCompetition c)
        {
            new RenameCompetitionValidator(ReadModelRepository).Validate(c);
            ChangeAgg(c.Id, agg => agg.RenameCompetition(c));
        }

    }
}
