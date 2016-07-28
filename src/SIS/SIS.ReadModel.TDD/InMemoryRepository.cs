using System;
using System.Collections.Generic;

namespace SIS.ReadModel.TDD
{
    public class InMemoryRepository<T> : IRepository<T> where T: IEntity
    {
        protected Dictionary<Guid, T> Store = new Dictionary<Guid, T>();

        public T GetById(Guid id)
        {
            if (Store.ContainsKey(id))
                return Store[id];
            return default(T);
        }

        public void Save(T obj)
        {
            Store[obj.Id] = obj;
        }
    }
}
