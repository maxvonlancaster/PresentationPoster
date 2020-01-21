using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TelegramAccess.Entities;

namespace TelegramAccess.Repositories.Interfaces
{
    public interface IPresentationRepository
    {
        Task Create(Presentation presentation);
        Task Edit(Presentation presentation);
        Task Delete(int id);
        Task<Presentation> Get(int id);
        Task<List<Presentation>> GetAll();
        bool PresentationExists(int id);
        Task<List<Presentation>> GetByUser(int userId);
        Task<Presentation> GetByName(string name);
    }
}
