using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using TodosAPI.Core.Entities;
using TodosAPI.Core.Interfaces;
using TodosAPI.Infrastructure.Data;


namespace TodosAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<User> GetByIdOrFailAsync(string userId)
        {   
            var user = await _context.Users.FindAsync(userId);
            if (user == null) {
                throw new BadHttpRequestException("Entidade Não encontrada");
            }
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }


    }
}
