using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace GirlBossSkeleton.Pages.RoleAdmin
{
    [Authorize(Roles = "Admins")]
    public class CreateModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;

        public CreateModel(Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    AddErrorsFromResult(result);
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