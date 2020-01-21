using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramAccess.Constants;
using TelegramAccess.Entities;
using TelegramAccess.Interfaces;
using TelegramAccess.Repositories.Interfaces;

namespace TelegramAccess.Services
{
    public class SyncService : ISyncService
    {
        private static readonly TelegramBotClient _client = new TelegramBotClient(Const.TelegramToken);
        private readonly IUserRepository _userRepository;
        private readonly IPresentationRepository _presentationRepository;

        private List<byte[]> _images;
        private int _current;
        private Guid _guid; // may be a bad design idea; INCAPSULATE!

        public List<byte[]> Images { get => _images; set => _images = value; }
        public int Current { get => _current; set => _current = value; }
        public Guid Guid { get => _guid; set => _guid = value; }

        public SyncService(IUserRepository userRepository, IPresentationRepository presentationRepository)
        {
            _userRepository = userRepository;
            _presentationRepository = presentationRepository;
        }

        public void StartReceiving() 
        {
            var cts = new CancellationTokenSource();
            var stoppingToken = cts.Token;

            _client.OnMessage += OnFirstMessage;
            _client.OnCallbackQuery += OnCallBack;

            Task.Run(() =>
            {
                _client.StartReceiving(cancellationToken: stoppingToken);
                Console.WriteLine("Started Receiving");
                while (!stoppingToken.IsCancellationRequested)
                {
                    _client.GetUpdatesAsync();
                }
            });
        }

        public byte[] GetImage() 
        {
            return _images[_current];
        } 

        private async void OnFirstMessage(object sender, MessageEventArgs e)
        {
            var user = await _userRepository.GetByTelegramId(e.Message.From.Username);
            if (user != null)
            {
                var presentations = (await _presentationRepository.GetByUser(user.Id)).Select(x => x.Name);
                var text = "Please select a presentation:";

                List<InlineKeyboardButton> buttons = new List<InlineKeyboardButton>();
                foreach (var presentation in presentations)
                {
                    InlineKeyboardButton button = new InlineKeyboardButton();
                    button.CallbackData = $"{user.Id}:{presentation}:{0}";
                    button.Text = presentation;
                }

                InlineKeyboardMarkup markup = new InlineKeyboardMarkup(buttons);
                await _client.SendTextMessageAsync(user.Id, text, replyMarkup: markup);
            }
            else 
            {
                string text = "Do you want to start receiving current presentation?";
                InlineKeyboardButton button = new InlineKeyboardButton();
                button.CallbackData = $"{user.Id}:{0}:{1}";
                InlineKeyboardMarkup markup = new InlineKeyboardMarkup(button);
                await _client.SendTextMessageAsync(e.Message.From.Id, text, replyMarkup: markup);
            }

        }

        private async void OnCallBack(object sender, CallbackQueryEventArgs e) 
        {
            string[] data = e.CallbackQuery.Data.Split(':');
            int userRole;
            if (int.TryParse(data[1], out userRole) && userRole == 0)
            {

            }
            else if (int.TryParse(data[1], out userRole) && userRole == 1) 
            {
            
            }
        }
    }
}
