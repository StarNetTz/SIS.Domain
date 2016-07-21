using StarNet.DDD;

namespace SIS
{
    public class CompetitionAggregateState : AggregateState
    {
        private string Name { get; set; }

        protected override void DelegateWhenToConcreteClass(IAggregateEvent ev)
        {
            When((dynamic)ev);
        }

        private void When(CompetitionAdded e)
        {
            Id = e.Id;
            Name = e.Name;
        }

        private void When(CompetitionRenamed e)
        {
            if (Name != e.Name)
                Name = e.Name;
        }
    }
}
