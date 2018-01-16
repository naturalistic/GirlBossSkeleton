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

namespace GirlBossSkeleton.Pages.ApproveMentors
{
    [Authorize(Roles = "Admins")]
    public class IndexModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public IndexModel(RoleManager<IdentityRole> roleMgr, UserManager<ApplicationUser> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;
        }

        [BindProperty]
        public RoleEditViewModel RoleEditViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            IdentityRole role = await roleManager.FindByNameAsync("Mentors");   // Perhaps rename mentors role ApprovedMentors 
            if (role == null)
            {
                return NotFound();
            }
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            foreach (ApplicationUser user in userManager.Users)
            {
                if (user.IsMentor)
                {
                    var list = await userManager.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                    list.Add(user);
                }
            }
            RoleEditViewModel = new RoleEditViewModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(RoleModificationViewModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user,
                        model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user,
                        model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return Page();
            }
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
