using DesafioEclipseworks.WebAPI.Application.Tasks.GetAll;
using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;
using DesafioEclipseworks.WebAPI.Tests.Mocks;
using FluentAssertions;

namespace DesafioEclipseworks.WebAPI.Tests.Application.Tasks
{
    public class GetAllTasksQueryHandlerTests
    {
        private readonly MockTaskRepository _mockTaskRepository;
        private readonly MockProjectRepository _mockProjectRepository;
        private readonly GetAllTasksQueryHandler _handler;

        public GetAllTasksQueryHandlerTests()
        {
            _mockTaskRepository = new();
            _mockProjectRepository = new();

            _handler = new(
                _mockTaskRepository.Object,
                _mockProjectRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenProjectDoesNotExist()
        {
            // Arrange
            var projectId = Guid.NewGuid();
            var query = new GetAllTasksQuery(projectId);

            _mockProjectRepository.MockGetProjectAsync(projectId, null);

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(TaskErrors.ProjectDoesNotExist(projectId));
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            var project = new Project(Guid.NewGuid(), "Fake Project");
            var query = new GetAllTasksQuery(project.Id);

            _mockProjectRepository.MockGetProjectAsync(project.Id, project);
            _mockTaskRepository.MockGetAllTasksAsync(project.Id);

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
