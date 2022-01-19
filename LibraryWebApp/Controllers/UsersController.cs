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
        public async Task<IActionResult> Index()
        {
            var k = await userManager.FindByIdAsync("f1870d81-bf26-4113-96aa-2e87adaa36e2");
            UserVM model = new UserVM()
            {
                ListOfCustomers = new List<IdentityUser>(),
                ListOfAdmins = new List<IdentityUser>(),
                ListOfWorkers = new List<IdentityUser>(),
            };
            foreach(var i in userManager.Users)
            {
                var user = await userManager.FindByIdAsync(i.Id);
                
                if(await userManager.IsInRoleAsync(i, WC.WorkerRole))
                {
                    model.ListOfWorkers.Add(i);
                }
                else if(await userManager.IsInRoleAsync(i, WC.AdminRole))
                {
                    model.ListOfAdmins.Add(i);
                }
                else
                {
                    model.ListOfCustomers.Add(i);
                }
            }
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
        }
    }
}
