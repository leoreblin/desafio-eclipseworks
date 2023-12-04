using DesafioEclipseworks.WebAPI.Abstractions.Data;
using DesafioEclipseworks.WebAPI.Abstractions.Messaging;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using DesafioEclipseworks.WebAPI.Domain.Shared;
using DesafioEclipseworks.WebAPI.Infrastructure.Errors;

namespace DesafioEclipseworks.WebAPI.Application.Tasks.Remove
{
    public class RemoveTaskCommandHandler : ICommandHandler<RemoveTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveTaskCommandHandler(ITaskRepository taskRepository, IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RemoveTaskCommand request, CancellationToken cancellationToken)
        {
            var taskFromDb = await _taskRepository.GetTaskAsync(request.TaskId);

            if (taskFromDb is null)
            {
                return TaskErrors.TaskDoesNotExist(request.TaskId);
            }

            _taskRepository.RemoveTask(taskFromDb);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
