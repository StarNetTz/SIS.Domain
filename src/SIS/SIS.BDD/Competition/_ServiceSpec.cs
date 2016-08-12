using Moq;
using SIS.ReadModel;
using StarNet.BDD;
using StarNet.DDD.PL;
using System;

namespace SIS.BDD
{
    internal class CompetitionServiceSpec : ApplicationServiceSpecification<ICommand, IEvent>
    {
        private ICompetitionReadModel CreateMock()
        {
            var mock = new Mock<ICompetitionReadModel>();
            mock.Setup(foo => foo.IsCompetitionNameUnique(It.IsAny<Guid>(), It.IsAny<string>())).Returns(true);
            mock.Setup(foo => foo.IsCompetitionNameUnique(It.IsAny<Guid>(), "IExist")).Returns(false);
            
            return mock.Object;
        }

        protected override IEvent[] ExecuteCommand(IEvent[] given, ICommand cmd)
        {
            var repository = new BDDInMemoryAggregateRepository();
            repository.Preload(cmd.Id, given);
            var svc = new CompetitionAppService(repository, CreateMock());
            svc.Execute(cmd);
            return repository.Appended ?? new IEvent[0];
        }
    }
}