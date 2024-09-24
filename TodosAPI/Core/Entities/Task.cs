using TodosAPI.Core.Entities;

public class Tarefa
{

    public int TarefaId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
}