using System;
using System.Collections.Generic;
using System.Text;
using TelegramAccess.Interfaces;
using Telegram.Bot;
using TelegramAccess.Constants;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Slides.v1;
using Google.Apis.Slides.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.IO;
using System.Threading;

namespace TelegramAccess.Services
{
    public class MessagingService : IMessagingService
    {
        private static readonly TelegramBotClient _client = new TelegramBotClient(Const.TelegramToken);

        private int lectureId;

        //private List<Page> _pages -> initialize in constructor;

        static string[] Scopes = { SlidesService.Scope.PresentationsReadonly };
        static string ApplicationName = "Google Slides API .NET Quickstart";

        public MessagingService(int lectureId)
        {
            this.lectureId = lectureId;
            string lectureAdress = Const.PresentationAdresses[lectureId];
            string stringId = lectureAdress.Split('/')[5];
            UserCredential credential;

        }

        public void ParsePage(int i) 
        {
            
        }

        public void SendText(string message) 
        {
        
        }

        public void SendPost(byte[] message) 
        {
        
        }
    }
}
