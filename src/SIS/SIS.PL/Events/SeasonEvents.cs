using StarNet.DDD.PL;
using System;

namespace SIS.PL.Events
{
    public class SeasonAdded : IEvent
    {
        public Guid Id { get; set; }
        public Guid CompetitionId { get; set; }
        public string Name { get; set; }
    }

    public class StageAdded : IEvent
    {
        public Guid Id { get; set; }
        public Guid SeasonId { get; set; }
        public string Name { get; set; }
    }
}
