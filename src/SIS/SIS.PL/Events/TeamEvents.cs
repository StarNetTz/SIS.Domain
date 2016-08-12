using StarNet.DDD.PL;
using System;

namespace SIS.PL.Events
{
    public class TeamAdded : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
