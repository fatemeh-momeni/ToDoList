using ToDoList.Application.DTOs;
using ToDoList.Domain;
using ToDoList.Infrastructure.Persistence;

namespace ToDoList.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskItem> _repository;

        public TaskService(IRepository<TaskItem> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _repository.GetAllAsync();
            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                IsCompleted = t.IsCompleted,
                DueDate = t.DueDate
            }).ToList();
        }

        public async Task<TaskDto?> GetTaskByIdAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate
            };
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = taskDto.Title,
                Description = taskDto.Description,
                IsCompleted = false,
                DueDate = taskDto.DueDate
            };

            await _repository.AddAsync(task);

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                DueDate = task.DueDate
            };
        }

        public async Task<bool> UpdateTaskAsync(Guid id, UpdateTaskDto taskDto)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return false;

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.IsCompleted = taskDto.IsCompleted;
            task.DueDate = taskDto.DueDate;

            await _repository.UpdateAsync(task);
            return true;
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
