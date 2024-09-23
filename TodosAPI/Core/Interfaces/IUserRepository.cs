using TodosAPI.Core.Entities;
using System.Threading.Tasks; // Usar o Task correto

namespace TodosAPI.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(string userId);
        Task<User?> GetByEmailAsync(string email);

        Task AddAsync(User user);
    }
}
