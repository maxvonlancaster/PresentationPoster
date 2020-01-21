using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelegramAccess.Entities;
using TelegramAccess.Repositories;
using TelegramAccess.Repositories.Interfaces;

namespace PresentationPoster.Controllers
{
    public class PresentationsController : Controller
    {
        private readonly IPresentationRepository _presentationRepository;

        public PresentationsController(IPresentationRepository presentationRepository)
        {
            _presentationRepository = presentationRepository;
        }

        // GET: Presentations
        public async Task<IActionResult> Index()
        {
            return View(await _presentationRepository.GetAll());
        }

        // GET: Presentations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _presentationRepository.Get((int)id);
            if (presentation == null)
            {
                return NotFound();
            }

            return View(presentation);
        }

        // GET: Presentations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Presentations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,File")] Presentation presentation)
        {
            if (ModelState.IsValid)
            {
                await _presentationRepository.Create(presentation);
                return RedirectToAction(nameof(Index));
            }
            return View(presentation);
        }

        // GET: Presentations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _presentationRepository.Get((int)id);
            if (presentation == null)
            {
                return NotFound();
            }
            return View(presentation);
        }

        // POST: Presentations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,File")] Presentation presentation)
        {
            if (id != presentation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _presentationRepository.Edit(presentation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentationExists(presentation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(presentation);
        }

        // GET: Presentations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentation = await _presentationRepository.Get((int)id);
            if (presentation == null)
            {
                return NotFound();
            }

            return View(presentation);
        }

        // POST: Presentations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _presentationRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PresentationExists(int id)
        {
            return _presentationRepository.PresentationExists(id);
        }
    }
}
