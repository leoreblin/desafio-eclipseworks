using DesafioEclipseworks.WebAPI.Application.Projects.Remove;
using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;
using DesafioEclipseworks.WebAPI.Tests.Mocks;
using FluentAssertions;

namespace DesafioEclipseworks.WebAPI.Tests.Application.Projects
{
    public class RemoveProjectCommandHandlerTests
    {
        private readonly MockProjectRepository _mockProjectRepository;
        private readonly MockUnitOfWork _mockUnitOfWork;

        private readonly RemoveProjectCommandHandler _handler;

        public RemoveProjectCommandHandlerTests()
        {
            _mockProjectRepository = new MockProjectRepository();
            _mockUnitOfWork = new MockUnitOfWork();

            _handler = new(_mockProjectRepository.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenProjectDoesNotExist()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var command = new RemoveProjectCommand(projectId);

            _mockProjectRepository.MockGetProjectAsync(projectId, null);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(ProjectErrors.ProjectDoesNotExist(projectId));
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenThereIsAnyTaskWithPendingStatus()
        {
            // Arrange
            var project = new Project(Guid.NewGuid(), "Fake Project");
            var command = new RemoveProjectCommand(project.Id);
            var tasks = new List<TaskEntity>
            {
                new("Fake Task 1", "Details 1", DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)), Status.Pending, Priority.High, project.Id),
                new("Fake Task 2", "Details 2", DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)), Status.InProgress, Priority.Medium, project.Id)
            };

            project.Tasks = tasks;

            _mockProjectRepository.MockGetProjectAsync(project.Id, project);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(ProjectErrors.ProjectWithPendingTasks(project.Id));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            var project = new Project(Guid.NewGuid(), "Fake Project");
            var command = new RemoveProjectCommand(project.Id);
            var tasks = new List<TaskEntity>
            {
                new("Fake Task 1", "Details 1", DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)), Status.InProgress, Priority.High, project.Id),
                new("Fake Task 2", "Details 2", DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)), Status.InProgress, Priority.Medium, project.Id),
                new("Fake Task 3", "Details 3", DateOnly.FromDateTime(DateTime.UtcNow.AddDays(10)), Status.Done, Priority.Medium, project.Id)
            };

            project.Tasks = tasks;

            var cancellationToken = new CancellationToken();

            _mockProjectRepository.MockGetProjectAsync(project.Id, project);
            _mockProjectRepository.MockRemoveProject(project);
            _mockUnitOfWork.MockSaveChangesAsync(cancellationToken);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
