using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramAccess.Interfaces;

namespace PresentationPoster.Controllers
{
    [Route("api/[controller]")]
    public class LectureController : Controller
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        [HttpGet]
        [Route("getLectures")]
        public ActionResult GetLectures()
        {
            return Json(_lectureService.GetLectures());
        }

        public ActionResult GetLecture(int id)
        {
            return Json(_lectureService.GetLecture(id));
        }
    }
}