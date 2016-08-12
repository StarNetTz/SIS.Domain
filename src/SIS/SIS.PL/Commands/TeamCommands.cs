using StarNet.DDD.PL;
using System;

namespace SIS.PL.Commands
{
    public class AddTeam : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
