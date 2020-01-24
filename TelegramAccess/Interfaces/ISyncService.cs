using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramAccess.Interfaces
{
    public interface ISyncService
    {
        //List<byte[]> Images { get; set; }
        //int Current { get; set; }
        //Guid Guid { get; set; }

        //byte[] GetImage();
        void StartReceiving();
    }
}
