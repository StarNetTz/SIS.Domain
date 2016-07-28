using System;
using System.Linq;

namespace SIS.ReadModel
{
    public interface IRepository<T> where T : IEntity
    {
        T GetById(Guid id);
        void Save(T obj);

    }
}
