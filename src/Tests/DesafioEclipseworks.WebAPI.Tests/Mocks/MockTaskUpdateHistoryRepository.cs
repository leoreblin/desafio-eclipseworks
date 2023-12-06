using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using Moq;

namespace DesafioEclipseworks.WebAPI.Tests.Mocks
{
    public class MockTaskUpdateHistoryRepository : Mock<ITaskUpdateHistoryRepository>
    {
        public MockTaskUpdateHistoryRepository() : base(MockBehavior.Strict)
        {            
        }

        public MockTaskUpdateHistoryRepository MockCreateHistoryAsync()
        {
            Setup(m => m.CreateHistoryAsync(It.IsAny<TaskUpdateHistory>()))
                .Returns(Task.CompletedTask);

            return this;
        }

        public MockTaskUpdateHistoryRepository MockGetUpdateHistoriesOverLast30Days(List<TaskUpdateHistory> output)
        {
            Setup(m => m.GetTaskUpdateHistoriesOverLast30Days())
                .ReturnsAsync(output);

            return this;
        }
    }
}
