using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using Moq;

namespace DesafioEclipseworks.WebAPI.Tests.Mocks
{
    public class MockTaskRepository : Mock<ITaskRepository>
    {
        public MockTaskRepository() : base(MockBehavior.Strict) { }

        public MockTaskRepository MockCreateTaskAsync()
        {
            Setup(m => m.AddTaskAsync(It.IsAny<TaskEntity>()))
                .Returns(Task.CompletedTask);

            return this;
        }

        public MockTaskRepository MockGetAllCompletedTasksAsync(List<TaskEntity> output)
        {
            Setup(m => m.GetAllCompletedTasksAsync())
                .ReturnsAsync(output);

            return this;
        }

        public MockTaskRepository MockGetTaskAsync(Guid id, TaskEntity? output)
        {
            Setup(m => m.GetTaskAsync(id))
                .ReturnsAsync(output);

            return this;
        }

        public MockTaskRepository MockGetAllTasksAsync(Guid projectId)
        {
            Setup(m => m.GetAllTasksByProjectIdAsync(projectId))
                .ReturnsAsync(It.IsAny<List<TaskEntity>>());

            return this;
        }
        public MockTaskRepository MockRemoveTask(TaskEntity task)
        {
            Setup(m => m.RemoveTask(task));

            return this;
        }

        public MockTaskRepository MockUpdateTask(TaskEntity task)
        {
            Setup(m => m.UpdateTask(task));

            return this;
        }
    }
}
