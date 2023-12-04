using DesafioEclipseworks.WebAPI.Abstractions.Data;
using Moq;

namespace DesafioEclipseworks.WebAPI.Tests.Mocks
{
    public class MockUnitOfWork : Mock<IUnitOfWork>
    {
        public MockUnitOfWork() : base(MockBehavior.Strict)
        {            
        }

        public MockUnitOfWork MockSaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Setup(m => m.SaveChangesAsync(cancellationToken))
                .Returns(Task.CompletedTask);

            return this;
        }
    }
}
