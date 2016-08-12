using Raven.Client;
using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIS.ReadModel
{
    public interface ICompetitionReadModel
    {
        bool IsCompetitionNameUnique(Guid id, string name);
        PaginatedResult<Competition> Search(PaginatedQuery request);
        Competition FindById(Guid id);
    }

    public class CompetitionReadModel : ICompetitionReadModel
    {
        public Competition FindById(Guid id)
        {
            Competition c = null;
            using (var ses = RavenGlobal.DocumentStore.OpenSession())
            {
                c = ses.Load<Competition>(id);
            }
            return c;
        }

        public bool IsCompetitionNameUnique(Guid id, string name)
        {
            Competition[] data = new Competition[0];
            using (var ses = RavenGlobal.DocumentStore.OpenSession())
            {
                data = ses.Query<Competition>().Where(x => x.Name.Equals(name)).ToArray();
            }
            foreach(var c in data)
            {
                if (c.Id != id) return false;
            }
            return true;
        }

        public PaginatedResult<Competition> Search(PaginatedQuery request)
        {
            PaginatedResult<Competition> result = new PaginatedResult<Competition>() { Data = new List<Competition>() };
            IRavenQueryable<Competition> qry = null;
            RavenQueryStatistics statsRef = new RavenQueryStatistics();
            using (var ses = RavenGlobal.DocumentStore.OpenSession())
            {
                qry = ses.Query<Competition>()
                    .Statistics(out statsRef)
                    .Search(x => x.Name, string.Format("{0}*", request.SearchString), escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                    .Skip(request.CurrentPage * request.PageSize)
                    .Take(request.PageSize);
            }
            result.Data = qry.ToList();
            result.TotalItems = statsRef.TotalResults;
            result.TotalPages = result.TotalItems / request.PageSize;
            if ((result.TotalItems % request.PageSize) > 0)
                result.TotalPages += 1;
            result.PageSize = request.PageSize;
            result.CurrentPage = request.CurrentPage;

            if (CurrentaPageIsOverflown(result))
            {
                return Search(new PaginatedQuery() { SearchString = request.SearchString, CurrentPage = 0, PageSize = request.PageSize });
            }
            return result;
        }

        private static bool CurrentaPageIsOverflown(PaginatedResult<Competition> result)
        {
            return (result.Data.Count == 0) && (result.TotalPages > 0);
        }
    }
}