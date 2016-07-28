using System;
using System.Linq;

namespace SIS.ReadModel
{
    public interface IProjector<T>
    {
        void Add(T view);
        void Update(Guid id, Action<T> updateAction);
    }
    public class CompetitionProjector : IProjector<Competition>
    {
        private IRepository<Competition> Repository;

        public CompetitionProjector(IRepository<Competition> repository)
        {
            Repository = repository;
        }

        public void Add(Competition view)
        {
            Repository.Save(view);
        }

        public void Update(Guid id, Action<Competition> updateAction)
        {
            var view = Repository.GetById(id);
            updateAction(view);
            Repository.Save(view);
        }
    }
}
