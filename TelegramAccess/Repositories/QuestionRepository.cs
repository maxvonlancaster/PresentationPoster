using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelegramAccess.Entities;
using TelegramAccess.Repositories.Interfaces;

namespace TelegramAccess.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        public Task Create(Question question)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<Question> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Question>> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool QuestionExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
