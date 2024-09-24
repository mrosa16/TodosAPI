using TodosAPI.Core.Entities;

public interface ITaskRepository
{
    Task<Tarefa?> GetTaskAsync(int taskId);
    Task<IEnumerable<Tarefa>> GetTasksAsync();
    Task<IEnumerable<Tarefa>> GetTasksByUserIdAsync(User user);

    Task CreateTaskAsync(Tarefa task);
    Task UpdateTaskAsync(Tarefa task);
    Task DeleteTaskAsync(int taskId);
    Task SaveChangesAsync(CancellationToken cancellationToken1);

    void ResetTracker();
}