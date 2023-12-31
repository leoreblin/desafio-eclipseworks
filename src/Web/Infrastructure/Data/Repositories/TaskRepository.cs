﻿using DesafioEclipseworks.WebAPI.Domain.Entities.Tasks;
using DesafioEclipseworks.WebAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioEclipseworks.WebAPI.Infrastructure.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTaskAsync(TaskEntity task)
        {
            await _context.AddAsync(task);
        }

        public async Task<List<TaskEntity>?> GetAllCompletedTasksAsync()
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.Status == Status.Done)
                .ToListAsync();
        }

        public async Task<List<TaskEntity>?> GetAllTasksByProjectIdAsync(Guid projectId)
        {
            return await _context.Tasks
                .AsNoTracking()
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<TaskEntity?> GetTaskAsync(Guid id)
        {
            return await _context.Tasks
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();
        }

        public void RemoveTask(TaskEntity task)
        {
            _context.Remove(task);
        }

        public void UpdateTask(TaskEntity task)
        {
            _context.Update(task);
        }
    }
}
