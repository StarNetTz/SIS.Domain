using StarNet.DDD.PL;
using System;

namespace SIS.PL.Commands
{
    public class AddSeason : ICommand
    {
        public Guid Id { get; set; }
        public Guid CompetitionId { get; set; }
        public string Name { get; set; }
    }

    public class AddStage : ICommand
    {
        public Guid Id { get; set; }
        public Guid SeasonId { get; set; }
        public string Name { get; set; }
    }
}
