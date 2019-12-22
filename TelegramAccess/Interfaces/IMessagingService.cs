using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramAccess.Interfaces
{
    public interface IMessagingService
    {
        void SendPost(byte[] message);
        void SendText(string message);
    }
}
