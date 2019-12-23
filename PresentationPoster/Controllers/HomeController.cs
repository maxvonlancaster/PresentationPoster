using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PresentationPoster.Models;
using TelegramAccess.Interfaces;
using TelegramAccess.Services;

namespace PresentationPoster.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILectureService _lectureService;
        private MessagingService messagingService;

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
        [Route("sendPage")]
        public void SendPage(int i) 
        {
        
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
