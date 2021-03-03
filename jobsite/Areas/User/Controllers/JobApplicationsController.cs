using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jobsite.Models;
using Microsoft.AspNetCore.Identity;

namespace jobsite.Areas.User.Controllers
{
    [Area("User")]
    public class JobApplicationsController : Controller
    {
        private readonly JobContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public IActionResult DownloadCV(int id)
        {
            var file = _context.CVs.Find(id);
            return File(file.Content, "application/pdf", $"{file.Title}");
        }

        public JobApplicationsController(JobContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: User/JobApplications
        public async Task<IActionResult> Index()
        {
            //var jobContext = _context.JobApplications.Include(j => j.CV).Include(j => j.Candidate).Include(j => j.JobPost);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var apps = ((Candidate)user).JobApplications;
            return View(apps.ToList());
        }

        // GET: User/JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .Include(j => j.CV)
                .Include(j => j.Candidate)
                .Include(j => j.JobPost)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }


        // GET: User/JobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            ViewData["CVId"] = new SelectList(_context.CVs, "Id", "Extension", jobApplication.CVId);
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Address", jobApplication.CandidateId);
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Description", jobApplication.JobPostId);
            return View(jobApplication);
        }

        // POST: User/JobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppDate,PrefDateToJoin,AppStatus,JobPostId,CandidateId,CVId")] JobApplication jobApplication)
        {
            if (id != jobApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobApplicationExists(jobApplication.Id))
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
            ViewData["CVId"] = new SelectList(_context.CVs, "Id", "Extension", jobApplication.CVId);
            ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Address", jobApplication.CandidateId);
            ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Description", jobApplication.JobPostId);
            return View(jobApplication);
        }

        private bool JobApplicationExists(int id)
        {
            return _context.JobApplications.Any(e => e.Id == id);
        }
    }
}
