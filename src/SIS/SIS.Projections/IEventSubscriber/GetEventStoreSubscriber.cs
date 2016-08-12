using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StarNet.DDD.PL;
using System;
using System.Configuration;
using System.Text;

namespace SIS.Projections
{
    public class GetEventStoreSubscriber : IEventSubscriber
    {
        private const string EventClrTypeHeader = "EventClrTypeName";
        private IEventStoreConnection Connection;

        private EventAppearedHandler EventAppeared;

        private IProjectionCheckpoint Checkpoint = null;

        private void EventAppearedInStore(EventStoreCatchUpSubscription subscription, ResolvedEvent @event)
        {
            var ev = DeserializeEvent(@event.Event.Metadata, @event.Event.Data);
            EventAppeared(ev as IEvent);
            Checkpoint.Update(@event.OriginalEventNumber);
        }

        private static object DeserializeEvent(byte[] metadata, byte[] data)
        {
            var eventClrTypeName = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventClrTypeHeader).Value;
            var jsonString = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject(jsonString, Type.GetType((string)eventClrTypeName));
        }



        public void Subscribe(EventAppearedHandler eventAppeared, string projectionName, string streamName)
        {
            Checkpoint = new RavenProjectionCheckpoint(projectionName);
            EventAppeared = eventAppeared;
            Connection = EventStoreConnection.Create(ConfigurationManager.ConnectionStrings["GetEventStoreConnection"].ToString());
            Connection.ConnectAsync().Wait();
            CatchUpSubscriptionSettings settings = new CatchUpSubscriptionSettings(500, 500, false, true);
            var subscription = Connection.SubscribeToStreamFrom(streamName, Checkpoint.Value, settings, EventAppearedInStore, null, null, new EventStore.ClientAPI.SystemData.UserCredentials("admin", "changeit"));
        }
    }
}
