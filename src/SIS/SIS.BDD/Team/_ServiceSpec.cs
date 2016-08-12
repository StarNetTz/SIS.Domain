using StarNet.BDD;
using StarNet.DDD.PL;

namespace SIS.BDD
{
    internal class TeamServiceSpec : ApplicationServiceSpecification<ICommand, IEvent>
    {
        protected override IEvent[] ExecuteCommand(IEvent[] given, ICommand cmd)
        {
            var repository = new BDDInMemoryAggregateRepository();
            repository.Preload(cmd.Id, given);
            var svc = new TeamAppService(repository);
            svc.Execute(cmd);
            return repository.Appended ?? new IEvent[0];
        }
    }
}