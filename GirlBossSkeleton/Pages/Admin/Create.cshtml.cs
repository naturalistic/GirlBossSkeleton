using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GirlBossSkeleton.Data;
using GirlBossSkeleton.Data.ViewModels;

namespace GirlBossSkeleton.Pages.Admin
{
    [Authorize(Roles = "Admins")]    public class CreateModel : PageModel
    {
        private UserManager<ApplicationUser> userManager;

        public CreateModel(UserManager<ApplicationUser> usrMgr)
        {
            userManager = usrMgr;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AppUserViewModel AppUserViewModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = AppUserViewModel.Name,
                    Email = AppUserViewModel.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, AppUserViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return Page();
        }
    }
}