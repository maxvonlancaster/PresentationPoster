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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly PresentationContext _context;

        public QuestionRepository(PresentationContext context)
        {
            _context = context;
        }

        public async Task Create(Question question)
        {
            _context.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Question question)
        {
            _context.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task<Question> Get(int id)
        {
            var question = await _context.Questions
                .FirstOrDefaultAsync(m => m.Id == id);
            return question;
        }

        public async Task<List<Question>> GetAll()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<List<Question>> GetByPresentation(int presentationId) 
        {
            return await _context.Questions
                .Include(q => q.Presentation)
                .Where(q => q.Presentation.Id == presentationId)
                .ToListAsync();
        }

        public bool QuestionExists(int id)
        {
            return _context.Questions.Any(e => e.Id == id);
        }
    }
}
