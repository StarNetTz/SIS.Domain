using Moq;
using SIS.PL;
using SIS.ReadModel;
using StarNet.BDD;
using StarNet.DDD;
using System;

namespace SIS.BDD
{
    internal class CompetitionServiceSpec : ApplicationServiceSpecification<ICommand, IAggregateEvent>
    {
        private ICompetitionReadModel CreateMock()
        {
            var mock = new Mock<ICompetitionReadModel>();
            mock.Setup(foo => foo.GetByName("IExist")).Returns(new Competition() { Id = Guid.NewGuid(), Name = "IExist"});
            return mock.Object;
        }

        protected override IAggregateEvent[] ExecuteCommand(IAggregateEvent[] given, ICommand cmd)
        {
            var repository = new BDDInMemoryAggregateRepository();
            repository.Preload(cmd.Id, given);
            var svc = new CompetitionAppService(repository, CreateMock());
            svc.Execute(cmd);
            return repository.Appended ?? new IAggregateEvent[0];
        }
    }
}