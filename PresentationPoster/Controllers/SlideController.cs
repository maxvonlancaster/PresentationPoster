using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramAccess.Entities;
using TelegramAccess.Interfaces;

namespace PresentationPoster.Controllers
{
    [Route("api/[controller]")]
    public class SlideController : Controller
    {
        private readonly ISyncService _syncService;

        public SlideController(ISyncService syncService)
        {
            _syncService = syncService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getSlide")]
        public ActionResult GetSlide() 
        {
            byte[] image = _syncService.GetImage();
            Guid guid = _syncService.Guid;
            ViewImage viewImage = new ViewImage() { Image = image };
            return Json(viewImage);
        }

        public class ViewImage 
        {
            public byte[] Image { get; set; }
            public Question Question { get; set; }
        }
    }
}