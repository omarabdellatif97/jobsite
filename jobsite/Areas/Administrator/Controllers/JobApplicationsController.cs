﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jobsite.Models;
using jobsite.Services;
using Microsoft.AspNetCore.Authorization;

namespace jobsite.Areas.Administrator.Controllers
{
    // Edit and Details only here for admin
    [Area("Administrator")]
    [Authorize("IsAdmin")]
    public class JobApplicationsController : Controller
    {
        private readonly JobContext _context;
        private readonly IUnitOfWork unit;

        public JobApplicationsController(JobContext context,IUnitOfWork unit)
        {
            _context = context;
            this.unit = unit;
        }

        public async Task<IActionResult> DownloadCV(int id)
        {
            //var file = _context.CVs.Find(id);
            var file = await unit.CV.GetAsync(id);
            return File(file.Content, "application/pdf", $"{file.Title}");
        }


        // GET: Admin/JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var jobApplication = await _context.JobApplications
            //    .Include(j => j.Candidate)
            //    .Include(j => j.JobPost)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var jobApplication = await unit.JobApplications.GetAsync(id.Value);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // GET: Admin/JobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var jobApplication = await _context.JobApplications
            //    .Include(j => j.Candidate)
            //    .Include(j => j.JobPost)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var jobApplication = await unit.JobApplications.GetAsync(id.Value);
            if (jobApplication == null)
            {
                return NotFound();
            }
            //ViewData["CVId"] = new SelectList(_context.CVs, "Id", "Extension", jobApplication.CVId);
            //ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Address", jobApplication.CandidateId);
            //ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Description", jobApplication.JobPostId);
            return View(jobApplication);
        }






        // POST: Admin/JobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AppStatus appStatus)
        {
            //var jobApplication = await _context.JobApplications
            //    .Include(j => j.Candidate)
            //    .Include(j => j.JobPost)
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var jobApplication = await unit.JobApplications.GetAsync(id);



            if (jobApplication == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                jobApplication.AppStatus = appStatus;
                //await _context.SaveChangesAsync();

                await unit.JobApplications.SaveAsync();

                return RedirectToAction(nameof(Details), new {id = jobApplication.Id});
            }
            return View(jobApplication);

        }


        private bool JobApplicationExists(int id)
        {
            return _context.JobApplications.Any(e => e.Id == id);
        }

        #region Not Required
        // GET: Admin/JobApplications
        //public async Task<IActionResult> Index()
        //{
        //    var jobContext = _context.JobApplications.Include(j => j.CV).Include(j => j.Candidate).Include(j => j.JobPost);
        //    return View(await jobContext.ToListAsync());
        //}


        //// GET: Admin/JobApplications/Create
        //public IActionResult Create()
        //{
        //    ViewData["CVId"] = new SelectList(_context.CVs, "Id", "Extension");
        //    ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Address");
        //    ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Description");
        //    return View();
        //}

        //// POST: Admin/JobApplications/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,AppDate,PrefDateToJoin,AppStatus,JobPostId,CandidateId,CVId")] JobApplication jobApplication)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(jobApplication);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CVId"] = new SelectList(_context.CVs, "Id", "Extension", jobApplication.CVId);
        //    ViewData["CandidateId"] = new SelectList(_context.Candidates, "Id", "Address", jobApplication.CandidateId);
        //    ViewData["JobPostId"] = new SelectList(_context.JobPosts, "Id", "Description", jobApplication.JobPostId);
        //    return View(jobApplication);
        //}


        //// GET: Admin/JobApplications/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var jobApplication = await _context.JobApplications
        //        .Include(j => j.CV)
        //        .Include(j => j.Candidate)
        //        .Include(j => j.JobPost)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (jobApplication == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(jobApplication);
        //}

        //// POST: Admin/JobApplications/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var jobApplication = await _context.JobApplications.FindAsync(id);
        //    _context.JobApplications.Remove(jobApplication);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        #endregion





    }
}
