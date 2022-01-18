#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BulkyBookWeb.Data;
using BulkyBookWeb.Models;

namespace BulkyBookWeb.Controllers
{
    public class MadVideoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MadVideoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get: PlayerView

        public async Task<IActionResult> Player(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var madVideo = await _context.MadVideos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (madVideo == null)
            {
                return NotFound();
            }

            return View(madVideo);
        }

        // GET: MadVideo
        public async Task<IActionResult> Index()
        {
            return View(await _context.MadVideos.ToListAsync());
        }


        //POST: search function for categories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = from m in _context.MadVideos
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Category!.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }

        // GET: MadVideo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var madVideo = await _context.MadVideos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (madVideo == null)
            {
                return NotFound();
            }

            return View(madVideo);
        }

        // GET: MadVideo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MadVideo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Link,Category")] MadVideo madVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(madVideo);
                await _context.SaveChangesAsync();
                TempData["Succes"] = "Category created succesfully";
                return RedirectToAction(nameof(Index));
            }
            return View(madVideo);
        }

        // GET: MadVideo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var madVideo = await _context.MadVideos.FindAsync(id);
            if (madVideo == null)
            {
                return NotFound();
            }
            return View(madVideo);
        }

        // POST: MadVideo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Link,Category")] MadVideo madVideo)
        {
            if (id != madVideo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(madVideo);
                    await _context.SaveChangesAsync();
                    TempData["Succes"] = "Edit saved!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MadVideoExists(madVideo.Id))
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
            return View(madVideo);
        }

        // GET: MadVideo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var madVideo = await _context.MadVideos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (madVideo == null)
            {
                return NotFound();
            }
            TempData["Succes"] = "Category deleted succesfully";
            return View(madVideo);
        }

        // POST: MadVideo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var madVideo = await _context.MadVideos.FindAsync(id);
            _context.MadVideos.Remove(madVideo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MadVideoExists(int id)
        {
            return _context.MadVideos.Any(e => e.Id == id);
        }
    }
}
