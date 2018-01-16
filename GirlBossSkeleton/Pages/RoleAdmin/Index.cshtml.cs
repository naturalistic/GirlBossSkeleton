using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace GirlBossSkeleton.Pages.RoleAdmin
{
    [Authorize(Roles = "Admins")]
    public class IndexModel : PageModel
    {
        private RoleManager<IdentityRole> roleManager;

        public IndexModel(Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }

        public IEnumerable<IdentityRole> Roles { get; set; }

        public void OnGet()
        {
            Roles = roleManager.Roles;
        }
    }
}