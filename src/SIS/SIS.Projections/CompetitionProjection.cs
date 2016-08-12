using SIS.PL.Events;
using SIS.ReadModel;
using StarNet.DDD.PL;

namespace SIS.Projections
{
    public class CompetitionProjection
    {
        IEventSubscriber Observer;
        IDocumentWriter<Competition> DocumentWriter;

        public CompetitionProjection(IEventSubscriber observer, IDocumentWriter<Competition> documentWriter)
        {
            Observer = observer;
            DocumentWriter = documentWriter;
            Observer.Subscribe(Observer_EventAppeared, "Competition", "CompetitionEvents");
        }

        private void Observer_EventAppeared(IEvent e)
        {
            When((dynamic)e);
        }

        public void When(CompetitionAdded e)
        {
            DocumentWriter.Add(new Competition() { Id = e.Id, Name = e.Name });
        }

        public void When(CompetitionRenamed e)
        {
            DocumentWriter.Update(e.Id, x => x.Name = e.Name);
        }
    }
}
