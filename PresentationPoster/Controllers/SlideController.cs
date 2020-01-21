using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    }
}