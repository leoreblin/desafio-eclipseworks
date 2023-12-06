using DesafioEclipseworks.WebAPI.Application.Projects.Create;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;
using DesafioEclipseworks.WebAPI.Tests.Mocks;
using FluentAssertions;

namespace DesafioEclipseworks.WebAPI.Tests.Application.Projects
{
    public class CreateProjectCommandHandlerTests
    {
        private readonly MockProjectRepository _mockProjectRepository;
        private readonly MockUnitOfWork _mockUnitOfWork;

        private readonly CreateProjectCommandHandler _handler;

        public CreateProjectCommandHandlerTests()
        {
            _mockProjectRepository = new MockProjectRepository();
            _mockUnitOfWork = new MockUnitOfWork();

            _handler = new(
                _mockProjectRepository.Object,
                _mockUnitOfWork.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnError_WhenProjectNameIsNullOrEmpty()
        {
            // Arrange
            var projectName = string.Empty;
            var userId = Guid.NewGuid();
            var command = new CreateProjectCommand(userId, projectName);

            // Act
            var result = await _handler.Handle(command, new CancellationToken());

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeEquivalentTo(ProjectErrors.ProjectNameIsNullOrEmpty);
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccess()
        {
            // Arrange
            string projectName = "Fake Project";
            var userId = Guid.NewGuid();
            CreateProjectCommand command = new(userId, projectName);

            CancellationToken cancellationToken = new();

            _mockProjectRepository.MockCreateProjectAsync();
            _mockUnitOfWork.MockSaveChangesAsync(cancellationToken);

            // Act
            var result = await _handler.Handle(command, cancellationToken);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
