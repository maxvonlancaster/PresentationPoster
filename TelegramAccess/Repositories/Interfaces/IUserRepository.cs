using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelegramAccess.Entities;

namespace TelegramAccess.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task Delete(int id);
        Task Edit(User user);
        Task<User> Get(int id);
        Task<List<User>> GetAll();
        Task<User> GetByTelegramId(string telegId);
        Task<User> GetByUserName(string userName);
        bool UserExists(string telegId);
    }
}
