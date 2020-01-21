using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramAccess.Entities;
using TelegramAccess.Repositories.Interfaces;

namespace TelegramAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PresentationContext _context;

        public UserRepository(PresentationContext context)
        {
            _context = context;
        }

        public async Task Create(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Get(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByTelegramId(string telegId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Telegram == telegId);
        }

        public bool UserExists(string telegId)
        {
            return _context.Users.Any(e => e.Telegram == telegId);
        }
    }
}
