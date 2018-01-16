using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using GirlBossSkeleton.Data;

namespace GirlBossSkeleton.Pages.Admin
{
    [Authorize(Roles = "Admins")]
    public class DetailsModel : PageModel
    {
        private UserManager<ApplicationUser> userManager;

        public DetailsModel(UserManager<ApplicationUser> usrMgr)
        {
            userManager = usrMgr;
        }

        public ApplicationUser AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            AppUser = await userManager.FindByIdAsync(id);
            if (AppUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
