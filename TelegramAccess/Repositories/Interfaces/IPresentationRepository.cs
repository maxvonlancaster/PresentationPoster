using System;
using System.Collections.Generic;
using System.Text;
using TelegramAccess.Entities;

namespace TelegramAccess.Repositories.Interfaces
{
    public interface IPresentationRepository
    {
        void Create(Presentation presentation);
        void Edit(int id, Presentation presentation);
        void Delete(int id);
        Presentation Get(int id);
        List<Presentation> GetAll();
    }
}
