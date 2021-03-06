using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public ReservationController(AppDbContext db, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
        [Authorize(Roles = WC.AdminRole + "," + WC.WorkerRole +","+WC.CustomerRole)]
        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            IEnumerable<Reservation> reservationBooks = _db.Reservation.Include(x => x.ReservationBook).ThenInclude(x => x.Book).Where(x => x.IdClient == user.Id).Include(x=>x.Fee);
            return View(reservationBooks);
        }
        [Authorize(Roles = WC.AdminRole + "," + WC.WorkerRole)]
        public IActionResult Delete(int id)
        {
            var reservation = _db.Reservation.Find(id);
            _db.Reservation.Remove(reservation);
            foreach (var reservationBook in _db.ReservationBook.Where(x => x.IdReservation == id))
            {
                _db.ReservationBook.Remove(reservationBook);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(IndexAdmin));
        }
        [Authorize(Roles = WC.AdminRole + "," + WC.WorkerRole)]
        public IActionResult IndexAdmin()
        {
            IEnumerable<Client> listOfClients = _db.Client
                .Include(x => x.Reservation)
                .ThenInclude(x=>x.ReservationBook)
                .ThenInclude(x=>x.Book)
                .Where(x=>x.Reservation.Count>0)
                .Include(x=>x.Reservation)
                .ThenInclude(x=>x.Fee);
            return View(listOfClients);
        }
        [Authorize(Roles = WC.AdminRole + "," + WC.WorkerRole + "," + WC.CustomerRole)]
        public IActionResult Cancel(int id)
        {
            var reservation = _db.Reservation.Find(id);
            if(reservation.Status == LibraryWebApp.ReservationStatus.Cancelled || reservation.Status == LibraryWebApp.ReservationStatus.Taken || reservation.Status == LibraryWebApp.ReservationStatus.Returned)
            {
                return NotFound();
            }
            reservation.Status = LibraryWebApp.ReservationStatus.Cancelled;
            _db.Reservation.Update(reservation);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //GET RETURN
        [Authorize(Roles = WC.AdminRole + "," + WC.WorkerRole)]
        public IActionResult ReturnBook(int id)
        {
            var reservation = _db.Reservation.Find(id);
            return View(reservation);
        }
        //POST - Return
        [Authorize(Roles = WC.AdminRole + "," + WC.WorkerRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReturnBook(Reservation reservationForm)
        {
            if (ModelState.IsValid)
            {
                var reservation = _db.Reservation.Find(reservationForm.Id);
                reservation.DateOfReturn = reservationForm.DateOfReturn;
                reservation.Status = LibraryWebApp.ReservationStatus.Returned;
                _db.Reservation.Update(reservation);
                _db.SaveChanges();
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(reservationForm);
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = WC.AdminRole + "," + WC.WorkerRole)]
        public IActionResult ReservationStatus(Reservation reservation)
        {
            var result = _db.Reservation.Find(reservation.Id);
            result.Status = reservation.Status;
            if (ModelState.IsValid)
            {
                _db.Reservation.Update(result);
                _db.SaveChanges();
                return RedirectToAction(nameof(IndexAdmin));
            }
            return View(result);
        }
    }
}
