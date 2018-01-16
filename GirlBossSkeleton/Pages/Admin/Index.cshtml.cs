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
    public class IndexModel : PageModel
    {
        private UserManager<ApplicationUser> userManager;

        public IndexModel(UserManager<ApplicationUser> usrMgr)
        {
            userManager = usrMgr;
        }

        public IEnumerable<ApplicationUser> AppUsers { get;set; }

        public void OnGet()
        {
            AppUsers = userManager.Users;
        }
    }
}
