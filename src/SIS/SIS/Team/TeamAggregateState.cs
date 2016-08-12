using SIS.PL.Events;
using StarNet.DDD;
using StarNet.DDD.PL;

namespace SIS
{
    public class TeamAggregateState : AggregateState
    {
        private string Name { get; set; }

        protected override void DelegateWhenToConcreteClass(IEvent ev)
        {
            When((dynamic)ev);
        }

        private void When(TeamAdded e)
        {
            Id = e.Id;
            Name = e.Name;
        }
    }
}
