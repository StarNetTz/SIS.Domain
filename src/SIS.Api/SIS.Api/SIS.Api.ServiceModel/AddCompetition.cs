using System;
using ServiceStack;
using SIS.ReadModel;
using System.Collections.Generic;

namespace SIS.Api.ServiceModel
{
    [Route("/competition/add", Verbs = "POST")]
    public class AddCompetition : IReturn<CommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    [Route("/competition/rename", Verbs = "POST")]
    public class RenameCompetition : IReturn<CommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    [Route("/competitions/{SearchString}/{CurrentPage}/{PageSize}")]
    public class FindCompetitions : IReturn<PaginatedResult<Competition>>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; } 
    }

    [Route("/competition/{Id}")]
    public class FindCompetition : IReturn<Competition>
    {
        public string Id { get; set; }
    }

    [Route("/hello/{Name}")]
    public class Hello : IReturn<string>
    {
        public string Name { get; set; }
    }
}