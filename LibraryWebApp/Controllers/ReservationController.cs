using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> userManager;
        public ReservationController(AppDbContext db, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            IEnumerable<Reservation> reservationBook =_db.Reservation.Include(x => x.ReservationBook).ThenInclude(x=>x.Book).Where(x=>x.IdClient == user.Id);
            return View(reservationBook);
        }
        public IActionResult Delete(int id)
        {
            var obj = _db.Reservation.Find(id);
            _db.Reservation.Remove(obj);
            foreach (var reservationBook in _db.ReservationBook.Where(x => x.IdReservation == id))
            {
                _db.ReservationBook.Remove(reservationBook);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
