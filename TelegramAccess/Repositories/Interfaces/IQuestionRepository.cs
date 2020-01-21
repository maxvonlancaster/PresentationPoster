using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelegramAccess.Entities;

namespace TelegramAccess.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task Create(Question question);
        Task Edit(Question question);
        Task Delete(int id);
        Task<Question> Get(int id);
        Task<List<Question>> GetAll();
        bool QuestionExists(int id);
    }
}
