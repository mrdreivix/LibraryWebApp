using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        public UsersController(AppDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            var users = userManager.Users;
            return View(users);
        }
    }
}
