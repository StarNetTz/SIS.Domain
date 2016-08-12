using StarNet.DDD.PL;
using System;

namespace SIS.PL.Events
{
    public class MatchScheduled : IEvent
    {
        public Guid Id { get; set; }
        public Guid StageId { get; set; }
        public int Round { get; set; }
        public DateTime Time { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
    }
}
