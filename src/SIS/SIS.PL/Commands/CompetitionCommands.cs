using StarNet.DDD.PL;
using System;

namespace SIS.PL.Commands
{
    public class AddCompetition : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class RenameCompetition : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
