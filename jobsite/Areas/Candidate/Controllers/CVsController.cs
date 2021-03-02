using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jobsite.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace jobsite.Areas.Candidate.Controllers
{
    [Area("Candidate")]
    public class CVsController : Controller
    {
        private readonly JobContext _context;

        public CVsController(JobContext context)
        {
            _context = context;
        }

        public IActionResult Download(int id)
        {
            var file = _context.CVs.Find(id);
            //return File(file.Content, "application/pdf", file.Title);
            return File(file.Content, "application/octet-stream", file.Title);
        }

        // GET: Candidate/CVs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CVs.Select(cv=> new CVInfo() { Id=cv.Id , Title=cv.Title }).ToListAsync());
        }

        // GET: Candidate/CVs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CVs.Select(cv => new CVInfo() { Id = cv.Id, Title = cv.Title })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // GET: Candidate/CVs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidate/CVs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CVInfo cvInfo, IFormFile file)
        {
            var cv = new CV() { Title=cvInfo.Title};
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                cv.Content = reader.ReadBytes((int)file.Length);
                cv.Extension = Path.GetExtension(file.FileName);
                //var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                //var fileName = parsedContentDisposition.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.Add(cv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cvInfo);
        }

        // GET: Candidate/CVs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CVs.FindAsync(id);
            if (cV == null)
            {
                return NotFound();
            }
            return View(cV);
        }

        // POST: Candidate/CVs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Extension")] CV cV)
        {
            if (id != cV.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cV);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CVExists(cV.Id))
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
            return View(cV);
        }

        // GET: Candidate/CVs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cV = await _context.CVs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cV == null)
            {
                return NotFound();
            }

            return View(cV);
        }

        // POST: Candidate/CVs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cV = await _context.CVs.FindAsync(id);
            _context.CVs.Remove(cV);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CVExists(int id)
        {
            return _context.CVs.Any(e => e.Id == id);
        }
    }
}
