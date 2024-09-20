namespace TodosAPI.Core.Entities
{
    public class Tarefa
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int UserId { get; set; }

    }
}
