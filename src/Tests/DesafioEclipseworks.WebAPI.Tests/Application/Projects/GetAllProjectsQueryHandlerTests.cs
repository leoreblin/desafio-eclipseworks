using DesafioEclipseworks.WebAPI.Application.Projects.GetAll;
using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Tests.Mocks;
using FluentAssertions;

namespace DesafioEclipseworks.WebAPI.Tests.Application.Projects
{
    public class GetAllProjectsQueryHandlerTests
    {
        private readonly MockProjectRepository _mockProjectRepository;
        private readonly GetAllProjectsQueryHandler _handler;

        public GetAllProjectsQueryHandlerTests()
        {
            _mockProjectRepository = new MockProjectRepository();

            _handler = new GetAllProjectsQueryHandler(_mockProjectRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnAllProjectsByUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            GetAllProjectsQuery query = new(userId);

            var projects = new List<Project>
            {
                new Project(userId, "Project A"),
                new Project(userId, "Project B"),
                new Project(userId, "Project C")
            };

            _mockProjectRepository.MockGetAllUserProjectsAsync(userId, projects);

            // Act
            var result = await _handler.Handle(query, new CancellationToken());

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value?.Count.Should().Be(3);
        }
    }
}
