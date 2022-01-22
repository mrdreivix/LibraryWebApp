using LibraryWebApp.Data;
using LibraryWebApp.Models;
using LibraryWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace LibraryWebApp.Controllers
{ 
    [Authorize]
    public class ReservationCartController : Controller
    {
        private readonly AppDbContext _db;

        private readonly UserManager<IdentityUser> userManager;

        public ReservationCartController(AppDbContext db, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            List<ReservationCartEntry> reservationCartList = new List<ReservationCartEntry>();
            if(HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart)!=null
                && HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart).Count() > 0)
            {
                reservationCartList = HttpContext.Session.Get<List<ReservationCartEntry>>(WC.SessionCart);
            }
            List<int> bookInCart = reservationCartList.Select(i => i.BookIdInCart).ToList();
            IEnumerable<Book> bookList = _db.Book.Where(u => bookInCart.Contains(u.Id));

            return View(bookList);
        }
        public IActionResult Delete(int id)
        {
            List<ReservationCartEntry> reservationCartList = new List<ReservationCartEntry>();
            if (HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart).Count() > 0)
            {
                reservationCartList = HttpContext.Session.Get<List<ReservationCartEntry>>(WC.SessionCart);
            }
            reservationCartList.Remove(reservationCartList.FirstOrDefault(u => u.BookIdInCart == id));
            HttpContext.Session.Set(WC.SessionCart, reservationCartList);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CreateReservation()
        {
            List<ReservationCartEntry> reservationCartList = new List<ReservationCartEntry>();
            if (HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart).Count() > 0)
            {
                reservationCartList = HttpContext.Session.Get<List<ReservationCartEntry>>(WC.SessionCart);
            }
            List<int> bookInCart = reservationCartList.Select(i => i.BookIdInCart).ToList();
            var todayDate = DateTime.Now;
            var userName = HttpContext.User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            Reservation reservation = new Reservation()
            {
                DateOfReservation = todayDate,
                PlannedDateOfReturn = todayDate.AddMonths(+1),
                IdClient= user.Id,
            };
            _db.Reservation.Add(reservation);
            _db.SaveChanges();
            var p = reservation.Id;
            foreach(var i in bookInCart)
            {
                ReservationBook reservationBook = new ReservationBook()
                {
                    IdReservation = p,
                    IdBook = i
                };
                _db.ReservationBook.Add(reservationBook);
            }
            _db.SaveChanges();
            reservationCartList.Clear();
            HttpContext.Session.Set(WC.SessionCart, reservationCartList);
            return RedirectToAction(nameof(Index), "Home");
        }
        
    }
}
