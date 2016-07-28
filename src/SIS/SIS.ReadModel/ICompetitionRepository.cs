using SIS.ReadModel;
using System;

namespace SIS
{
    public interface ICompetitionReadModel
    {
        Competition GetById(Guid id);
        Competition GetByName(string name);
    }
}