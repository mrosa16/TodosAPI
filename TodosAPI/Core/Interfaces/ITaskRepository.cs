namespace TodosAPI.Core.Interfaces
{
    public interface ITaskRepository
    {
        Task<Task> getByIdAsync(int id, int userId);
        Task<IEnumerable<Task>> getAllByUserIdAsync(int userId);
        Task AddAsync(Task task);
        Task UpdateAsync(Task task);
        Task DeleteAsync(int id, int userId);
    }
}
