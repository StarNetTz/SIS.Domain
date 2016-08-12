using StarNet.DDD.PL;
using System;

namespace SIS.PL.Events
{
    public class CompetitionAdded : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CompetitionRenamed : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
