using DesafioEclipseworks.WebAPI.Domain.Entities.Projects;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using Moq;

namespace DesafioEclipseworks.WebAPI.Tests.Mocks
{
    public class MockProjectRepository : Mock<IProjectRepository>
    {
        public MockProjectRepository() : base(MockBehavior.Strict)
        {            
        }

        public MockProjectRepository MockCreateProjectAsync()
        {
            Setup(m => m.AddProjectAsync(It.IsAny<Project>()))
                .Returns(Task.CompletedTask);

            return this;
        }

        public MockProjectRepository MockGetAllUserProjectsAsync(Guid userId, List<Project> output)
        {
            Setup(m => m.GetAllUserProjectsAsync(userId))
                .ReturnsAsync(output);

            return this;
        }

        public MockProjectRepository MockGetProjectAsync(Guid id, Project? output)
        {
            Setup(m => m.GetProjectAsync(id))
                .ReturnsAsync(output);

            return this;
        }

        public MockProjectRepository MockRemoveProject(Project project)
        {
            Setup(m => m.RemoveProject(project));

            return this;
        }
    }
}
