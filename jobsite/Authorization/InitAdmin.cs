using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using jobsite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;

namespace jobsite.Authorization
{
    public static class InitAdmin
    {
        public static async void SeedUsers(UserManager<ApplicationUser> _userManager, string email, string pwd)
        {
            if (!_userManager.Users.Any(u => u.Discriminator == "Admin"))
            {
                var user = new Admin
                {
                    UserName = email,
                    Email = email,
                    Address = "",
                    BirthDate = DateTime.Now,
                    Gender = Gender.Male,
                    Name = "Admin",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, pwd);
                if (!result.Succeeded)
                {
                    throw new Exception("Default Admin is Required");
                }
            }
        }
    }
}
