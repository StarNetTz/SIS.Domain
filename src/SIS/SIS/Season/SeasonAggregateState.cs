using SIS.PL.Events;
using StarNet.DDD;
using StarNet.DDD.PL;

namespace SIS
{
    public class SeasonAggregateState : AggregateState
    {
        private string Name { get; set; }

        protected override void DelegateWhenToConcreteClass(IEvent ev)
        {
            When((dynamic)ev);
        }

        private void When(SeasonAdded e)
        {
            Id = e.Id;
            Name = e.Name;
        }

        private void When(StageAdded e)
        {
        }
    }
}
