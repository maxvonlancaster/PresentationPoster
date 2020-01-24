using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TelegramAccess.Entities;
using TelegramAccess.Interfaces;
using TelegramAccess.Models;

namespace PresentationPoster.Controllers
{
    [Route("api/[controller]")]
    public class SlideController : Controller
    {
        private readonly ISyncService _syncService;
        private readonly IImageHolder _imageHolder;

        public SlideController(ISyncService syncService, IImageHolder imageHolder)
        {
            _syncService = syncService;
            _imageHolder = imageHolder;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("getSlide")]
        public ActionResult GetSlide() 
        {
            byte[] image = _imageHolder.GetImage();
            Guid guid = _imageHolder.Guid;
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