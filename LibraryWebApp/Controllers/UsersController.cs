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
            UserVM model = new UserVM()
            {
                ListOfCustomers = new List<ApplicationUser>(),
                ListOfAdmins = new List<ApplicationUser>(),
                ListOfWorkers = new List<Worker>(),
            };
            
            foreach(var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user, WC.WorkerRole))
                {
                    model.ListOfWorkers.Add((Worker)user);
                }
                else if(await userManager.IsInRoleAsync(user, WC.AdminRole))
                {
                    model.ListOfAdmins.Add((ApplicationUser)user);
                }
                else
                {
                    model.ListOfCustomers.Add((ApplicationUser)user);
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
                if (_db.Reservation.Where(x => x.IdClient == id).Count() > 0)
                {
                    foreach (Reservation reservation in _db.Reservation.Where(x => x.IdClient == id))
                    {
                        foreach (ReservationBook reservationBook in reservation.ReservationBook)
                        {
                            _db.ReservationBook.Remove(reservationBook);
                        }
                        _db.Reservation.Remove(reservation);
                    }
                    _db.SaveChanges();
                }
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
        //GET - EDIT
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _db.Worker.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Worker obj)
        {
            var user = await userManager.FindByIdAsync(obj.Id);
            var worker = (Worker)user;
            worker.Earnings = obj.Earnings;
            if (ModelState.IsValid)
            {
                _db.Worker.Update(worker);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }
    }
}
