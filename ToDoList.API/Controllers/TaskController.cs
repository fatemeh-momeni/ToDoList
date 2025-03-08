using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Domain;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IRepository<TaskItem> _taskRepository;

        public TaskController(IRepository<TaskItem> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // ✅ دریافت همه وظایف (GET: /api/task)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return Ok(tasks);
        }

        // ✅ دریافت یک وظیفه خاص (GET: /api/task/{id})
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound("وظیفه پیدا نشد.");
            }
            return Ok(task);
        }

        // ✅ ایجاد یک وظیفه جدید (POST: /api/task)
        [HttpPost]
        public async Task<ActionResult> CreateTask(TaskItem task)
        {
            if (task == null)
                return BadRequest("اطلاعات نامعتبر است.");

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        // ✅ به‌روزرسانی وظیفه (PUT: /api/task/{id})
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTask(Guid id, TaskItem task)
        {
            if (id != task.Id)
                return BadRequest("شناسه نامعتبر است.");

            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
                return NotFound("وظیفه موردنظر یافت نشد.");

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.IsCompleted = task.IsCompleted;
            existingTask.DueDate = task.DueDate;

            await _taskRepository.UpdateAsync(existingTask);
            await _taskRepository.SaveChangesAsync();

            return NoContent();
        }

        // ✅ حذف یک وظیفه (DELETE: /api/task/{id})
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
                return NotFound("وظیفه موردنظر یافت نشد.");

            await _taskRepository.DeleteAsync(id);
            await _taskRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
