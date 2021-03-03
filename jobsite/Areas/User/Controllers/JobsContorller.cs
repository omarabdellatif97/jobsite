﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jobsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using jobsite.Annotations;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace jobsite.Areas.Controllers.User
{
    [Area("User")]
    [Authorize("IsCandidate")]
    public class JobsController : Controller
    {
        private readonly JobContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

  

        public JobsController(JobContext context, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Admin/JobPosts
        public async Task<IActionResult> Index()
        {
            var jobContext = _context.JobPosts.Include(j => j.Department).Include(j => j.Applications);
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
                .Include(d => d.Applications)
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

            var jobPost = await _context.JobPosts.Include(j => j.Keywords).FirstOrDefaultAsync(j => j.Id == id);
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
                    //_context.Attach(jobPost);
                    _context.Entry(jobPost).State = EntityState.Modified;
                    //_context.Update(jobPost);
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




        private async Task LoadAsync(ApplicationUser user, BinderModel binderModel)
        {
            var userName = await _userManager.GetUserNameAsync(user);

            binderModel.Username = userName;
            if (((Candidate)user).CV != null)
            {
                binderModel.CV = ((Candidate)user).CV.Title;
            }
            else
            {
                binderModel.CV = null;
            }
            binderModel.Input = ((Candidate)user).Educations;
        }





        // GET: Admin/JobPosts/Delete/5
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

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (((Candidate)user).JobApplications.Any(J => J.JobPostId == jobPost.Id))
            {
                TempData["AppMessage"] = "You have already applied.";
                return RedirectToAction(nameof(Details), new { id = jobPost.Id });
            }


            BinderModel binderModel = new BinderModel();
            binderModel.PrefDateToJoin = null;
            await LoadAsync(user, binderModel);
            


            return View(binderModel);
        }


        public class BinderModel
        {
            public IFormFile FormFile { get; set; }

            [Required]
            [CurrentDate(ErrorMessage = "Date must be after or equal to current date")]
            [DataType(DataType.Date)]
            public DateTime? PrefDateToJoin { get; set; }

            public ICollection<Education> Input { get; set; }
            //public ICollection<jobsite.Models.Education> Input { get; set; };
            public String Username { get; set; }
            public String CV { get; set; }

        }


        // POST: Admin/JobPosts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Apply(int id, [Bind("PrefDateToJoin,FormFile")] BinderModel binderModel, ICollection<Education> Input)
        {
            var jobPost = await _context.JobPosts.FindAsync(id);

            var context = this.Request;

            if (jobPost == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (((Candidate)user).JobApplications.Any(J => J.JobPostId == jobPost.Id))
            {
                TempData["AppMessage"] = "You have already applied.";
                return RedirectToAction(nameof(Details), new { id = jobPost.Id });
            }



            bool cvUploaded = (binderModel.FormFile != null);

            if (!cvUploaded && ((Candidate)user).CV == null)
            {
                //binderModel.StatusMessage = "You must provide cv to apply.";
                ModelState.AddModelError("CV", "You must provide cv to apply.");
                binderModel = new BinderModel();
                await LoadAsync(user, binderModel);
                //TempData["StatusMessage"] = "You must provide cv to apply.";
                return View(binderModel);
            }
            byte[] Content = new byte[0];

            if (cvUploaded)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await binderModel.FormFile.CopyToAsync(memoryStream);

                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152 * 4)
                    {
                        Content = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                binderModel = new BinderModel();
                await LoadAsync(user, binderModel);
                return View(binderModel);
            }

            var cnt = ((Candidate)user).Educations.Count;
            ((Candidate)user).Educations = Input;

            if (cvUploaded)
            {
                CV cv = new CV();
                cv.Title = binderModel.FormFile.FileName;
                cv.Content = Content;
                cv.Extension = Path.GetExtension(binderModel.FormFile.FileName);
                if (cv.Extension.Length > 20)
                {
                    cv.Extension = "unknown";
                }
                ((Candidate)user).CV = cv;
            }

            var updt = await _userManager.UpdateAsync(user);
            if (!updt.Succeeded)
            {
                TempData["AppMessage"] = "Unexpected error when trying to update educations.";
                //binderModel.StatusMessage = "Unexpected error when trying to update educations.";
                return RedirectToAction();
            }


            var newApp = new JobApplication()
            {
                JobPostId = jobPost.Id,
                CandidateId = user.Id,
                CV = ((Candidate)user).CV,
                AppStatus = AppStatus.AppReceived,
                PrefDateToJoin = (DateTime)binderModel.PrefDateToJoin,
                AppDate = DateTime.Now
            };

            _context.Add(newApp);
            await _context.SaveChangesAsync();

            TempData["AppMessage"] = "We Have Received Your Application.";
            return RedirectToAction(nameof(Details), new { id = jobPost.Id });
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
