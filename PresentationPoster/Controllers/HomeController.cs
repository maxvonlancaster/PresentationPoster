using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PresentationPoster.Models;
using TelegramAccess.Interfaces;
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

        public HomeController(ILogger<HomeController> logger, ILectureService lectureService)
        {
            _logger = logger;
            _lectureService = lectureService;
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

        [HttpGet]
        [Route("getLectures")]
        public ActionResult GetLectures() 
        {
            return Json(_lectureService.GetLectures());        
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
            if (item.I == 1) 
            {
                _messagingService = new MessagingService(_lectureId);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class ItemSent
    {
        public int I { get; set; }
    }
}
