using SIS.ReadModel;

namespace SIS.Projections
{
    public class ProjectorService
    {
        public void Start()
        {
            var Subscriber = new GetEventStoreSubscriber();
            var Repository = new RavenDocumentWriter<Competition>();
            var Projection = new CompetitionProjection(Subscriber, Repository);
        }

        public void Stop()
        {

        }
    }
}
