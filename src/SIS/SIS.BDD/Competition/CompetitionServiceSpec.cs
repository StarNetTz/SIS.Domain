using SIS.PL;
using StarNet.BDD;
using StarNet.DDD;

namespace SIS.BDD
{
    internal class CompetitionServiceSpec : ApplicationServiceSpecification<ICommand, IAggregateEvent>
    {
        protected override IAggregateEvent[] ExecuteCommand(IAggregateEvent[] given, ICommand cmd)
        {
            var repository = new BDDInMemoryAggregateRepository();
            repository.Preload(cmd.Id, given);
            var svc = new CompetitionAppService(repository);
            svc.Execute(cmd);
            return repository.Appended ?? new IAggregateEvent[0];
        }
    }
}