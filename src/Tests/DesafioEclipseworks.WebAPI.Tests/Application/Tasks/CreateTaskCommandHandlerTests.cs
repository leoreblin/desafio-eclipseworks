using DesafioEclipseworks.WebAPI.Application.Tasks.Create;
using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.DTO;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;
using DesafioEclipseworks.WebAPI.Tests.Mocks;
using FluentAssertions;
using Moq;

namespace DesafioEclipseworks.WebAPI.Tests.Application.Tasks
{
    public class CreateTaskCommandHandlerTests
    {
        private readonly MockTaskRepository _mockTaskRepository;
        private readonly MockProjectRepository _mockProjectRepository;
        private readonly MockUnitOfWork _mockUnitOfWork;

        private readonly CreateTaskCommandHandler _handler;

        public CreateTaskCommandHandlerTests()
        {
            _mockTaskRepository = new MockTaskRepository();
            _mockProjectRepository = new MockProjectRepository();
            _mockUnitOfWork = new MockUnitOfWork();

            _handler = new(
                _mockTaskRepository.Object,
                _mockProjectRepository.Object,
                _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenProjectDoesNotExist()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var command = new CreateTaskCommand(projectId, It.IsAny<CreateTaskRequest>());

            _mockProjectRepository.MockGetProjectAsync(projectId, null);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Should().BeEquivalentTo(TaskErrors.ProjectDoesNotExist(projectId));
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenProjectAlreadyHas20Tasks()
        {
            // Arrange
            var project = new Project(Guid.NewGuid(), "Fake Project");
            var tasks = new List<TaskEntity>();

            for (int i = 0; i < 20; i++)
            {
                tasks.Add(It.IsAny<TaskEntity>());
            }

            project.Tasks = tasks;

            var command = new CreateTaskCommand(project.Id, It.IsAny<CreateTaskRequest>());

            _mockProjectRepository.MockGetProjectAsync(project.Id, project);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Should().BeEquivalentTo(TaskErrors.ProjectWithTaskLimitMaximum(project.Id));
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenDueDateIsLessThanCurrentDate()
        {
            // Arrange
            var project = new Project(Guid.NewGuid(), "Fake Project");
            project.Tasks = new List<TaskEntity>();

            var createTaskRequest = new CreateTaskRequest(
                "Fake Title", "Fake Detail", DateTime.UtcNow.AddDays(-2),
                Priority.Medium);

            var command = new CreateTaskCommand(project.Id, createTaskRequest);
            var cancellationToken = new CancellationToken();

            _mockProjectRepository.MockGetProjectAsync(project.Id, project);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Should().BeEquivalentTo(TaskErrors.InvalidDueDate);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            var project = new Project(Guid.NewGuid(), "Fake Project");
            project.Tasks = new List<TaskEntity>();

            var createTaskRequest = new CreateTaskRequest(
                "Fake Title", "Fake Detail", DateTime.UtcNow.AddDays(10),
                Priority.Medium);

            var command = new CreateTaskCommand(project.Id, createTaskRequest);
            var cancellationToken = new CancellationToken();

            _mockProjectRepository.MockGetProjectAsync(project.Id, project);
            _mockTaskRepository.MockCreateTaskAsync();
            _mockUnitOfWork.MockSaveChangesAsync(cancellationToken);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _mockTaskRepository.VerifyAll();
            _mockProjectRepository.VerifyAll();
            _mockUnitOfWork.VerifyAll();
        }
    }
}
