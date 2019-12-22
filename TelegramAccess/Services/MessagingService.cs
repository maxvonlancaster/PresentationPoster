using System;
using System.Collections.Generic;
using System.Text;
using TelegramAccess.Interfaces;
using Telegram.Bot;
using TelegramAccess.Constants;

namespace TelegramAccess.Services
{
    public class MessagingService : IMessagingService
    {
        private static readonly TelegramBotClient _client = new TelegramBotClient(Const.TelegramToken);

        public void SendText(string message) 
        {
        
        }

        public void SendPost(byte[] message) 
        {
        
        }
    }
}
