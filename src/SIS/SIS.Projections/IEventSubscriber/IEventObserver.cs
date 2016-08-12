namespace SIS.Projections
{
    public interface IEventSubscriber
    {
        void Subscribe(EventAppearedHandler EventAppeared, string projectionName, string streamName);
    }
}
