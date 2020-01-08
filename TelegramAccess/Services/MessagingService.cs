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
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace TelegramAccess.Services
{
    public class MessagingService
    {
        private static MessagingService instance;

        private static readonly TelegramBotClient _client = new TelegramBotClient(Const.TelegramToken);

        private static int _lectureId;
        private static string _presentationId;
        private static SlidesService _slidesService;

        private static IList<Page> _pages; 

        static string[] Scopes = { SlidesService.Scope.PresentationsReadonly };
        static string ApplicationName = "Google Slides API .NET Quickstart";

        private static ChatId _chat = new ChatId(Const.ChatId);


        public static void NewInstance(int lectureId)
        {
            _lectureId = lectureId;
            string lectureAdress = Const.PresentationAdresses[lectureId];
            string stringId = lectureAdress.Split('/')[5];
            UserCredential credential = null;

            try
            {
                using (var stream =
                    new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                    // The file token.json stores the user's access and refresh tokens, and is created
                    // automatically when the authorization flow completes for the first time.
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                    Console.WriteLine("Credential file saved to: " + credPath);
                }
            }
            catch (Exception ex) 
            {
                
            }

            // Create Google Slides API service.
            var service = new SlidesService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            _slidesService = service;

            // Define request parameters.
            _presentationId = stringId;
            PresentationsResource.GetRequest request = service.Presentations.Get(_presentationId);

            // Prints the number of slides and elements in a sample presentation:
            // https://docs.google.com/presentation/d/1EAYk18WDjIG-zp_0vLm3CsfQh_i8eXc67Jo2O9C6Vuc/edit
            Presentation presentation = request.Execute();
            IList<Page> slides = presentation.Slides;
            _pages = slides;
            var thumbnail = new PresentationsResource.PagesResource.GetThumbnailRequest(service, _presentationId, slides[0].ObjectId);
            Thumbnail v = thumbnail.Execute();

            string imageUrl = v.ContentUrl;
            System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
            webRequest.AllowWriteStreamBuffering = true;
            webRequest.Timeout = 30000;

            System.Net.WebResponse webResponse = webRequest.GetResponse();

            System.IO.Stream streamI = webResponse.GetResponseStream();
            SendStream(streamI);
        }

        public static void ParsePage(int i) 
        {
            var thumbnail = new PresentationsResource.PagesResource.GetThumbnailRequest(_slidesService, _presentationId, _pages[i].ObjectId);
            Thumbnail v = thumbnail.Execute();

            string imageUrl = v.ContentUrl;
            System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(imageUrl);
            webRequest.AllowWriteStreamBuffering = true;
            webRequest.Timeout = 30000;

            System.Net.WebResponse webResponse = webRequest.GetResponse();

            System.IO.Stream streamI = webResponse.GetResponseStream();
            SendStream(streamI);
            if (i == _pages.Count - 1) 
            {
                Reset();
            }
        }

        public static void Reset() 
        {
            _lectureId = -1;
        }

        public void SendText(string message) 
        {
            _client.SendTextMessageAsync(_chat, message);
        }

        public void SendPost(byte[] message) 
        {
            using (Stream stream = new MemoryStream(message))
            {
                InputOnlineFile file = new InputOnlineFile(stream);
                _client.SendPhotoAsync(_chat, file);
            }
        }

        public static void SendStream(Stream stream) 
        {
            InputOnlineFile file = new InputOnlineFile(stream);
            _client.SendPhotoAsync(_chat, file);
        }
    }
}
