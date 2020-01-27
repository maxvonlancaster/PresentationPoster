using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PresentationPoster.Models;
using TelegramAccess.Entities;
using TelegramAccess.Interfaces;
using TelegramAccess.Repositories.Interfaces;
using TelegramAccess.Services;

namespace PresentationPoster.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILectureService _lectureService;
        private MessagingService _messagingService;
        private int _lectureId = 0;

        private readonly IPresentationRepository _presentationRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;


        public HomeController(ILogger<HomeController> logger, 
            ILectureService lectureService,
            IPresentationRepository presentationRepository,
            IQuestionRepository questionRepository,
            IUserRepository userRepository)
        {
            _logger = logger;
            _lectureService = lectureService;
            _presentationRepository = presentationRepository;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Presentation(int id) 
        {
            TelegramAccess.Models.Lecture lecture = _lectureService.GetLecture(id);
            _lectureId = id;
            return View(lecture);
        }

        public async Task<IActionResult> Questions(int presentationId) 
        {
            var questions = await _questionRepository.GetByPresentation(presentationId);
            return View(questions);
        }

        [HttpGet]
        [Route("getLectures")]
        public async Task<ActionResult> GetLectures() 
        {
            return Json(await _presentationRepository.GetByUser(User.Identity.Name));        
        }

        [HttpGet]
        [Route("getLecture")]
        public ActionResult GetLecture(int id)
        {
            return Json(_lectureService.GetLecture(id));
        }

        [HttpPost]
        //[Route("Presentation/Home/SendPage/{i}")]
        public void SendPage([FromBody]ItemSent item) 
        {
            if (item.i == 1) 
            {
                MessagingService.NewInstance(item.lectureId);
            }
            MessagingService.ParsePage(item.i);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [HttpPost]
        public async Task<IActionResult> FileUpload(ICollection<IFormFile> files)
        {
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        var fileName = file.FileName;
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        var userName = User.Identity.Name;
                        var user = await _userRepository.GetByUserName(userName);
                        var presentation = new Presentation() { User = user, File = fileBytes, Name = fileName };
                        await _presentationRepository.Create(presentation);
                    }
                }
            }
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> TelegramId(ICollection<string> telegramId) 
        {
            var userName = User.Identity.Name;
            var user = await _userRepository.GetByUserName(userName);
            user.Telegram = telegramId.First();
            await _userRepository.Edit(user);
            return View("Inex");
        }

        [HttpPost]
        [Route("PostQuestion")]
        public async Task<IActionResult> PostQuestion([FromBody] QuestionModel questionModel)
        {
            var presentation = await _presentationRepository.Get(questionModel.Presentation);
            Question question = new Question() 
            { 
                Presentation = presentation, 
                Correct = questionModel.Correct,
                Options = questionModel.Options,
                Sentence = questionModel.Sentence,
                Slide = questionModel.Slide
            };
            await _questionRepository.Create(question);
            return await Questions(question.Presentation.Id);
        }
    }

    public class QuestionModel 
    {
        public int Presentation { get; set; }
        public string Sentence { get; set; }
        public string Options { get; set; }
        public int Correct { get; set; }
        public int Slide { get; set; }
    }
    

    public class ItemSent
    {
        public int i { get; set; }
        public int lectureId { get; set; }
    }
}
