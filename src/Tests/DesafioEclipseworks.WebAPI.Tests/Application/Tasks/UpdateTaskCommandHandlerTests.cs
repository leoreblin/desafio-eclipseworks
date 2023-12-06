using DesafioEclipseworks.WebAPI.Application.Tasks.Update;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;
using DesafioEclipseworks.WebAPI.Tests.Mocks;
using FluentAssertions;

namespace DesafioEclipseworks.WebAPI.Tests.Application.Tasks
{
    public class UpdateTaskCommandHandlerTests
    {
        private readonly MockTaskRepository _mockTaskRepository;
        private readonly MockTaskUpdateHistoryRepository _mockTaskUpdateHistoryRepository;
        private readonly MockUnitOfWork _mockUnitOfWork;

        private readonly UpdateTaskCommandHandler _handler;

        public UpdateTaskCommandHandlerTests()
        {
            _mockTaskRepository = new MockTaskRepository();
            _mockTaskUpdateHistoryRepository = new MockTaskUpdateHistoryRepository();
            _mockUnitOfWork = new MockUnitOfWork();

            _handler = new(
                _mockTaskRepository.Object,
                _mockUnitOfWork.Object,
                _mockTaskUpdateHistoryRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenTaskDoesNotExist()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var command = new UpdateTaskCommand(
                UserId: Guid.NewGuid(),
                TaskId: taskId,
                Title: "Fake Title",
                Details: "Fake Details",
                DueDate: DateTime.UtcNow.AddDays(5),
                Status: Domain.Entities.Tasks.Status.InProgress);

            _mockTaskRepository.MockGetTaskAsync(taskId, null);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Should().BeEquivalentTo(TaskErrors.TaskDoesNotExist(taskId));
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenDueDateIsLessThanCurrentDate()
        {
            // Arrange
            var task = new TaskEntity(
                title: "Fake Title",
                details: "Fake Details",
                dueDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                status: Status.InProgress,
                priority: Priority.Medium,
                projectId: Guid.NewGuid());

            var command = new UpdateTaskCommand(
                UserId: Guid.NewGuid(),
                TaskId: task.Id,
                Title: "Updated Fake Title",
                Details: "Updated Fake Details",
                DueDate: DateTime.UtcNow.AddDays(-1),
                Status: Status.InProgress);

            _mockTaskRepository.MockGetTaskAsync(task.Id, task);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Should().BeEquivalentTo(TaskErrors.InvalidDueDate);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            var task = new TaskEntity(
                title: "Fake Title",
                details: "Fake Details",
                dueDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)),
                status: Status.InProgress,
                priority: Priority.Medium,
                projectId: Guid.NewGuid());

            var command = new UpdateTaskCommand(
                UserId: Guid.NewGuid(),
                TaskId: task.Id,
                Title: "Updated Fake Title",
                Details: "Updated Fake Details",
                DueDate: DateTime.UtcNow.AddDays(15),
                Status: Status.InProgress);

            var cancellationToken = new CancellationToken();

            _mockTaskRepository
                .MockGetTaskAsync(task.Id, task)
                .MockUpdateTask(task);

            _mockTaskUpdateHistoryRepository
                .MockCreateHistoryAsync();

            _mockUnitOfWork.MockSaveChangesAsync(cancellationToken);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
