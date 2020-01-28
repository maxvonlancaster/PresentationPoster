using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            return View(/*"Index"*/);
        }

        [HttpGet]
        [Route("getSlide")]
        public ActionResult GetSlide() 
        {
            byte[] image = _imageHolder.GetImage();
            Guid guid = _imageHolder.Guid;
            MemoryStream stream = new MemoryStream(image);
            string converted = Convert.ToBase64String(stream.ToArray());
            ViewImage viewImage = new ViewImage() { Image = converted,  Guid = guid };

            return Json(viewImage);
        }

        [HttpGet]
        [Route("getGuid")]
        public ActionResult GetGuid() 
        {
            Guid guid = _imageHolder.Guid;
            return Json(guid);
        }

        [HttpPost]
        [Route("UploadFile")]
        public IActionResult UploadFile([FromBody]FileUpload file) 
        {
            var r = Request;
            return View("Home/Index");
        }

        public class ViewImage 
        {
            public string Image { get; set; }
            public Question Question { get; set; }
            public Guid Guid { get; set; }
            public bool Last { get; set; }
        }

        public class FileUpload 
        {
            public IFormFile data { get; set; }
        }
    }
}