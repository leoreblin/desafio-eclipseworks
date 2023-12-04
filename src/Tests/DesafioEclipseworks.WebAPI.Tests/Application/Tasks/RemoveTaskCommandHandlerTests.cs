using DesafioEclipseworks.WebAPI.Application.Tasks.Remove;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;
using DesafioEclipseworks.WebAPI.Tests.Mocks;
using FluentAssertions;

namespace DesafioEclipseworks.WebAPI.Tests.Application.Tasks
{
    public class RemoveTaskCommandHandlerTests
    {
        private readonly MockTaskRepository _mockTaskRepository;
        private readonly MockUnitOfWork _mockUnitOfWork;

        private readonly RemoveTaskCommandHandler _handler;

        public RemoveTaskCommandHandlerTests()
        {
            _mockTaskRepository = new MockTaskRepository();
            _mockUnitOfWork = new MockUnitOfWork();

            _handler = new(
                _mockTaskRepository.Object,
                _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenTaskDoesNotExist()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var command = new RemoveTaskCommand(taskId);

            _mockTaskRepository.MockGetTaskAsync(taskId, null);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Should().BeEquivalentTo(TaskErrors.TaskDoesNotExist(taskId));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            var task = new TaskEntity(
                title: "Fake Task",
                details: "Fake details",
                dueDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                status: Status.Pending,
                priority: Priority.Low,
                projectId: Guid.NewGuid());

            var command = new RemoveTaskCommand(task.Id);

            var cancellationToken = new CancellationToken();

            _mockTaskRepository
                .MockGetTaskAsync(task.Id, task)
                .MockRemoveTask(task);

            _mockUnitOfWork.MockSaveChangesAsync(cancellationToken);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
