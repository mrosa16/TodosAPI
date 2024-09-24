namespace TodosAPI.Infrastructure.Dto_s
{
    
    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string? Status { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

}
