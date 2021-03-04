using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jobsite.Models;
using Microsoft.AspNetCore.Authorization;
using jobsite.Services;

namespace jobsite.Areas.Controllers.Administrator
{
    [Area("Administrator")]
    [Authorize("IsAdmin")]
    public class JobPostsController : Controller
    {
        private readonly JobContext _context;
        private readonly IUnitOfWork unit;

        public JobPostsController(JobContext context,IUnitOfWork unit)
        {
            _context = context;
            this.unit = unit;
        }

        // GET: Admin/JobPosts
        public async Task<IActionResult> Index()
        {
            //var jobContext = _context.JobPosts.Include(j => j.Department).Include(j => j.Applications);
            //var data = await jobContext.ToListAsync();
            
            var data = await unit.JobPosts.GetAllAsync();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(string jobsearch)
        {
            var search = jobsearch;
            return RedirectToAction(nameof(SearchResult),new { jobsearch=search });
        }


        public async Task<IActionResult> SearchResult(string jobsearch)
        {
            //var jobContext = _context.JobPosts.Include(j => j.Department).Include(j => j.Applications);
            //var data = await jobContext.ToListAsync();
            ViewData["jobsearch"] = jobsearch;
            var data = await unit.JobPosts.SearchAsync(jobsearch);
            return View(data.Where(d=> d.Status == JobPostStatus.Opened));
        }

        //GET: Admin/JobPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var jobPost = await _context.JobPosts
            //    .Include(d => d.Applications)
            //    .Include(j => j.Department)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var jobPost = await unit.JobPosts.GetAsync(id.Value);
            var apps = await unit.JobApplications.GetAllAsync(j=> j.JobPostId == id.Value);

            jobPost.Applications = apps;

            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }




        // GET: Admin/JobPosts/Create
        //public IActionResult Create()
        //{
        //    //ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name");
        //    ViewBag.DeptId = new SelectList(_context.Departments, "Id", "Name");
        //    return View();
        //}




        // GET: Admin/JobPosts/Create
        public IActionResult Create()
        {
            //ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name");
            //var dept = unit.Departments.GetAll();
            //ViewBag.DeptId = new SelectList(dept, "Id", "Name");
            ViewData["DeptId"] = new SelectList(unit.Departments.GetAll(), "Id", "Name");
            return View();
        }




        // POST: Admin/JobPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Description,Location,PostDate,Status,DeptId,KeywordsText")] JobPost jobPost)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(jobPost);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Description", jobPost.DeptId);
        //    return View(jobPost);
        //}



        // POST: Admin/JobPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JobPost jobPost)
        {
            jobPost.PostDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                unit.JobPosts.Add(jobPost);
                await unit.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name", jobPost.DeptId);
            ViewData["DeptId"] = new SelectList(unit.Departments.GetAllIEnumerable(), "Id", "Name", jobPost.DeptId);
            return View(jobPost);
        }







        //// GET: Admin/JobPosts/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var jobPost = await _context.JobPosts.Include(j=> j.Keywords).FirstOrDefaultAsync(j=> j.Id == id);
        //    if (jobPost == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Description", jobPost.DeptId);
        //    return View(jobPost);
        //}



        // GET: Admin/JobPosts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = unit.JobPosts.Get(id.Value);
            if (jobPost == null)
            {
                return NotFound();
            }
            ViewData["DeptId"] = new SelectList(unit.Departments.GetAllIEnumerable(), "Id", "Name", jobPost.DeptId);
            //ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name", jobPost.DeptId);
            return View(jobPost);
        }



        // POST: Admin/JobPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JobPost jobPost)
        {
            if (id != jobPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unit.JobPosts.Update(jobPost);
                    await unit.SaveAsync();
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
            //ViewData["DeptId"] = new SelectList(_context.Departments, "Id", "Name", jobPost.DeptId);
            ViewData["DeptId"] = new SelectList(unit.Departments.GetAllIEnumerable(), "Id", "Name", jobPost.DeptId);
            return View(jobPost);
        }


        // Close JobPost
        // GET: Admin/JobPosts/Delete/5
        public IActionResult Close(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = unit.JobPosts.Get(id.Value);
            if (jobPost == null)
            {
                return NotFound();
            }

            return View(jobPost);
        }

        // POST: Admin/JobPosts/Delete/5
        [HttpPost, ActionName("CloseConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CloseConfirmed(int id)
        {
            //var jobPost = await _context.JobPosts.FindAsync(id);
            //unit.JobPosts.Update(jobPost);
            //await _context.SaveChangesAsync();
            unit.JobPosts.closeJobPost(new JobPost() { Id = id });
            await unit.SaveAsync();
            return RedirectToAction(nameof(Index));
        }




        // GET: Admin/JobPosts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPost = unit.JobPosts.Get(id.Value);
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
            //var jobPost = await _context.JobPosts.FindAsync(id);
            //_context.JobPosts.Remove(jobPost);
            //await _context.SaveChangesAsync();

            var post = await unit.JobPosts.GetAsync(id);
            unit.JobPosts.Remove(post);
            await unit.SaveAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool JobPostExists(int id)
        {
            return _context.JobPosts.Any(e => e.Id == id);
        }
    }
}
