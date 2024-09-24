using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using TodosAPI.Core.Entities;
using TodosAPI.Core.Interfaces;
using TodosAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace TodosAPI.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public void ResetTracker()
        {
            _context.ChangeTracker.Clear();
        }
        public async Task<Tarefa?> GetTaskAsync(int tarefaId)
        {
            
            return await _context.Tasks
                       .Where(t => t.TarefaId == tarefaId)
                       .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTasksByUserIdAsync(User user)
        {
            Console.WriteLine($"Buscando tarefas para o UserId: {user.UserId}"); 

            var userID = user.UserId; 
            var tasksForUser = await _context.Tasks
                .Where(t => t.UserId == userID)
                .ToListAsync();

           
            return tasksForUser;
        }


        public async Task CreateTaskAsync(Tarefa task)
        {
            _context.Attach(task.User);
            _context.Tasks.Add(task);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Tarefa task)
        {
            if (task.Status == "Concluída")
            {
                task.CompletedAt = DateTime.UtcNow;
            }
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

     

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await GetTaskAsync(taskId);
            if (task != null)

            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
