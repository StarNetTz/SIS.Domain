using System;
using System.Linq;

namespace SIS.ReadModel
{
    public class PaginatedQuery
    {
        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}