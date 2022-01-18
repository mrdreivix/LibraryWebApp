using LibraryWebApp.Data;
using LibraryWebApp.Models;
using LibraryWebApp.Models.ViewModels;
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
        private readonly UserManager<IdentityUser> userManager;
        public UsersController(AppDbContext db, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            UserVM model = new UserVM 
            { 
                ListOfUsers = userManager.Users,
            };
            return View(model);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
            var users = userManager.Users;
            return View(users);
        }
    }
}
