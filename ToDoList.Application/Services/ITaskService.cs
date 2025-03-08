using ToDoList.Application.DTOs;

namespace ToDoList.Application.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto?> GetTaskByIdAsync(Guid id);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto taskDto);
        Task<bool> UpdateTaskAsync(Guid id, UpdateTaskDto taskDto);
        Task<bool> DeleteTaskAsync(Guid id);
    }
}
