namespace MyTask.Models
{
    public class Ticket
    {
        public string? UserId { get; set; }
        public Guid Id { get; set; }
        public string? Title{ get; set; }
        public string Status { get; set; } = "ToDo";
    }
}
