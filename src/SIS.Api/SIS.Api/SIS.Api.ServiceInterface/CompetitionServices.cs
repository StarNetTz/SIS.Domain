using System.Collections.Generic;
using ServiceStack;
using SIS.Api.ServiceModel;
using SIS.ReadModel;
using System;

namespace SIS.Api.ServiceInterface
{
    public class CompetitionServices : Service
    {
        public IMessageBus Bus { get; private set; }
        public ICompetitionReadModel ReadModel { get; private set; }

        public CompetitionServices(IMessageBus bus)
        {
            Bus = bus;
        }

        public object Any(AddCompetition request)
        {
            var cmd = request.ConvertTo<PL.Commands.AddCompetition>();
            Bus.Send(cmd);
            return new CommandResponse { Status = Status.OK, ErrorMessage = string.Empty };
        }

        public object Any(RenameCompetition request)
        {
            var cmd = request.ConvertTo<PL.Commands.RenameCompetition>();
            Bus.Send(cmd);
            return new CommandResponse { Status = Status.OK, ErrorMessage = string.Empty };
        }

        public object Any(FindCompetitions request)
        {
            var qry = request.ConvertTo<PaginatedQuery>();
            return ReadModel.Search(qry);
        }

        public object Any(FindCompetition request)
        {
            return ReadModel.FindById(Guid.Parse(request.Id));
        }
    }

    public class TestServices : Service
    {


        public TestServices()
        {
            //  Bus = bus;
        }

        public object Any(Hello request)
        {
            return string.Format("Hello {0}!", request.Name);
        }

      
    }
}