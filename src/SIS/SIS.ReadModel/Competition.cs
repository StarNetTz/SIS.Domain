using System;

namespace SIS.ReadModel
{
    public class Competition : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
