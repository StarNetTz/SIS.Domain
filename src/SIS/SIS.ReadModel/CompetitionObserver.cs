using SIS.PL;

namespace SIS.ReadModel
{
    public class CompetitionObserver : IEventObserver
    {
        IProjector<Competition> Projector;

        public CompetitionObserver(IProjector<Competition> projector)
        {
            Projector = projector;

        }

        public void EventAppeared(IEvent ev)
        {
            When((dynamic)ev);
        }

        void When(CompetitionAdded e)
        {
            var cmpt = new Competition() { Id = e.Id, Name = e.Name };
            Projector.Add(cmpt);
        }

        void When(CompetitionRenamed e)
        {
            Projector.Update(e.Id, (c) => { c.Name = e.Name; });
        }
    }
}
