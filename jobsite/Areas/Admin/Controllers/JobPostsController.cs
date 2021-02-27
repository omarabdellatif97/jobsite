using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jobsite.Models;

namespace jobsite.Areas.Admin
{
    [Area("Admin")]
    public class JobPostsController : Controller
    {
        private readonly JobContext _context;

        public JobPostsController(JobContext context)
        {
            _context = context;
        }

        // GET: Admin/JobPosts
        public async Task<IActionResult> Index()
        {
            var jobContext = _context.JobPosts.Include(j => j.Department);
            return View(await jobContext.ToListAsync());
        }

        // GET: Admin/JobPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // GET: Admin/JobPosts/Create
        public IActionResult Create()
        {
            //ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.DeptId = new SelectList(_context.Departments, "Id", "Name");
            return View();
        }

        // POST: Admin/JobPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Location,PostDate,Status,DeptId,KeywordsText")] JobPost jobPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Description", jobPost.DeptId);
            return View(jobPost);
        }

        // GET: Admin/JobPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts.Include(j=> j.Keywords).FirstOrDefaultAsync(j=> j.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }
            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Description", jobPost.DeptId);
            return View(jobPost);
        }

        // POST: Admin/JobPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Location,PostDate,Status,DeptId,KeywordsText")] JobPost jobPost)
        {
            if (id != jobPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostExists(jobPost.Id))
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
            ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Description", jobPost.DeptId);
            return View(jobPost);
        }

        // GET: Admin/JobPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = await _context.JobPosts
                .Include(j => j.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // POST: Admin/JobPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPost = await _context.JobPosts.FindAsync(id);
            _context.JobPosts.Remove(jobPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostExists(int id)
        {
            return _context.JobPosts.Any(e => e.Id == id);
        }
    }
}
