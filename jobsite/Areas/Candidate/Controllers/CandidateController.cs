using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jobsite.Areas.Candidate.Controllers
{
    public class CandidateController : Controller
    {
        public   ActionResult JobDetails()
        {
            return View();
        }
        public ActionResult Apply()
        {
            return View();
        }

    }

}
