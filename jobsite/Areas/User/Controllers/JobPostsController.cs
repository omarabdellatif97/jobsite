using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jobsite.Models;
using Microsoft.AspNetCore.Authorization;

namespace jobsite.Areas.User
{
    [Area("User")]
    [Authorize("IsCandidate")]
    public class JobPostsController : Controller
    {
        private readonly JobContext _context;

        public JobPostsController(JobContext context)
        {
            _context = context;
        }

        // GET: Candidate/JobPosts
        public async Task<IActionResult> Index()
        {
            var jobContext = _context.JobPosts.Include(j => j.Department);
            return View(await jobContext.ToListAsync());
        }

        // GET: Candidate/JobPosts/Details/5
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


        public async Task<IActionResult> Apply(int? id)
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


    }
}
