namespace ToDoList.Application.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompeleted { get; set; } = true;
        public DateTime DueDate { get; set; }
    }
}
