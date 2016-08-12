using NServiceBus;
using NServiceBus.Logging;
using SIS.PL.Commands;
using SIS.PL.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Application
{
    public class CompetitionHandler : IHandleMessages<AddCompetition>, IHandleMessages<RenameCompetition>
    {
        public ICompetitionAppService Svc { get; private set; }

        private static readonly ILog log = LogManager.GetLogger<CompetitionHandler>();
        public CompetitionHandler(ICompetitionAppService svc)
        {
            Svc = svc;
        }

        public void Handle(AddCompetition cmd)
        {
            try
            {
                Svc.Execute(cmd);
                log.InfoFormat("{0} {1}", cmd.Id, cmd.Name);
            }
            catch (AggregateException ex)
            {
                log.Error(ex.GetBaseException().Message);
            }

        }

        public void Handle(RenameCompetition cmd)
        {
            try
            {
                Svc.Execute(cmd);
                log.InfoFormat("{0} {1}", cmd.Id, cmd.Name);
            }
            catch (AggregateException ex)
            {
                log.Error(ex.GetBaseException().Message);
            }

        }
    }
}
