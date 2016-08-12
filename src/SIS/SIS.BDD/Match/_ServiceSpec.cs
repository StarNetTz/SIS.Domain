using StarNet.BDD;
using StarNet.DDD.PL;

namespace SIS.BDD
{
    internal class MatchServiceSpec : ApplicationServiceSpecification<ICommand, IEvent>
    {
        protected override IEvent[] ExecuteCommand(IEvent[] given, ICommand cmd)
        {
            var repository = new BDDInMemoryAggregateRepository();
            repository.Preload(cmd.Id, given);
            var svc = new MatchAppService(repository);
            svc.Execute(cmd);
            return repository.Appended ?? new IEvent[0];
        }
    }
}