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
    public class PresentationRepository : IPresentationRepository
    {
        private readonly PresentationContext _context;

        public PresentationRepository(PresentationContext context)
        {
            _context = context;
        }

        public async Task Create(Presentation presentation)
        {
            _context.Add(presentation);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var presentation = await _context.Presentations.FindAsync(id);
            _context.Presentations.Remove(presentation);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Presentation presentation)
        {
            _context.Update(presentation);
            await _context.SaveChangesAsync();
        }

        public async Task<Presentation> Get(int id)
        {
            var presentation = await _context.Presentations
                .FirstOrDefaultAsync(m => m.Id == id);
            return presentation;
        }

        public async Task<List<Presentation>> GetAll()
        {
            return await _context.Presentations.ToListAsync();
        }

        public async Task<List<Presentation>> GetByUser(string userName) 
        {
            var res = await _context.Presentations.Include(p => p.User) // REFACTOR THIS
                .Where(p => p.User.UserName == userName)
                .ToListAsync();
            return res;
        }

        public async Task<Presentation> GetByName(string name) 
        {
            return await _context.Presentations.Where(p => p.Name == name).FirstOrDefaultAsync();
        }

        public bool PresentationExists(int id) 
        {
            return _context.Presentations.Any(e => e.Id == id);
        }
    }
}
