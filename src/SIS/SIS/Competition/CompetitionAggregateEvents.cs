using StarNet.DDD;
using System;

namespace SIS
{
    public class CompetitionAdded : IAggregateEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CompetitionRenamed : IAggregateEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
