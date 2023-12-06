using DesafioEclipseworks.WebAPI.Abstractions.Data;
using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Shared;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;
using DesafioEclipseworks.WebAPI.Infrastructure.Helpers;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.Update
{
    public class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskUpdateHistoryRepository _taskUpdateHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork, ITaskUpdateHistoryRepository taskUpdateHistoryRepository)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
            _taskUpdateHistoryRepository = taskUpdateHistoryRepository;
        }

        public async Task<Result> Handle(
            UpdateTaskCommand request,
            CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskRepository.GetTaskAsync(request.TaskId);

            if (taskFromDb is null)
            {
                return TaskErrors.TaskDoesNotExist(request.TaskId);
            }

            if (request.DueDate < DateTime.Now)
            {
                return TaskErrors.InvalidDueDate;
            }
             
            TaskEntity updatedTask = new(request.Title, request.Details,
                DateOnly.FromDateTime(request.DueDate), request.Status, taskFromDb.Priority, taskFromDb.ProjectId);

            updatedTask.Id = taskFromDb.Id;

            var updatedProperties = ObjectComparer.GetChangedProperties(taskFromDb, updatedTask);

            TaskUpdateHistory history = new(
                updatedTask.Id, request.UserId, DateTime.UtcNow,
                $"Campos alterados: {string.Join(", ", updatedProperties)}");

            await _taskUpdateHistoryRepository.CreateHistoryAsync(history);

            taskFromDb.DueDate = DateOnly.FromDateTime(request.DueDate);
            taskFromDb.Status = request.Status;
            taskFromDb.Title = request.Title;
            taskFromDb.Details = request.Details;

            _taskRepository.UpdateTask(taskFromDb);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
