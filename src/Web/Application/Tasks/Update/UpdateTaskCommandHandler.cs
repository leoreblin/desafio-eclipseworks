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
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
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

            taskFromDb.DueDate = request.DueDate;
            taskFromDb.Status = request.Status;
            taskFromDb.Title = request.Title;
            taskFromDb.Details = request.Details;
             
            TaskEntity newTask = new(request.Title, request.Details,
                request.DueDate, request.Status, taskFromDb.Priority, taskFromDb.Id);

            _taskRepository.UpdateTaskAsync(taskFromDb);

            var updatedProperties = ObjectComparer.GetChangedProperties(taskFromDb, newTask);

            TaskUpdateHistory history = new(
                newTask.Id, request.UserId, DateTime.UtcNow,
                $"Campos alterados: {string.Join(", ", updatedProperties)}");

            //TODO: Save task update history to the database

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
