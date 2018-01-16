using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GirlBossSkeleton.Data;
using GirlBossSkeleton.Data.ViewModels;

namespace GirlBossSkeleton.Pages.FindMentor
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public IndexModel(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        public List<ApplicationUser> Mentors { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Mentors = new List<ApplicationUser>();
            foreach (ApplicationUser user in userManager.Users)
            {
                if (user.IsMentor && await userManager.IsInRoleAsync(user, "Mentors"))
                {
                    Mentors.Add(user);
                }
            }
            return Page();
        }

        

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
