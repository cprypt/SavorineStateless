using Microsoft.EntityFrameworkCore;
using Savorine.AsyncServer.Data;
using Savorine.AsyncServer.Interfaces;
using Savorine.AsyncServer.Models;

namespace Savorine.AsyncServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GameDbContext _context;
        public UserRepository(GameDbContext context) => _context = context;

        public async Task<User?> GetByUsernameAsync(string username) =>
            await _context.Users.SingleOrDefaultAsync(u => u.Username == username);

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}