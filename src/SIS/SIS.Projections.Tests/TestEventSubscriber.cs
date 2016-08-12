using System;
using StarNet.DDD.PL;

namespace SIS.Projections.Tests
{
    public class TestEventSubscriber : IEventSubscriber
    {
        EventAppearedHandler EventAppeared;
        public void Subscribe(EventAppearedHandler eventAppeared, string projectionName, string streamName)
        {
            EventAppeared = eventAppeared;
        }

        public void Publish(params IEvent[] events)
        {
            foreach (var evnt in events)
                EventAppeared(evnt);
        }
    }
}
