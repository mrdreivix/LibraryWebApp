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

        private readonly UserManager<IdentityUser> _userManager;

        public ReservationCartController(AppDbContext db, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
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
            List<int> booksInCart = reservationCartList.Select(i => i.BookIdInCart).ToList();
            var userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            Reservation reservation = new Reservation()
            {
                DateOfReservation = DateTime.Now,
                PlannedDateOfReturn = DateTime.Now.AddMonths(+1),
                IdClient= user.Id,
            };
            _db.Reservation.Add(reservation);
            _db.SaveChanges();
            foreach(var book in booksInCart)
            {
                ReservationBook reservationBook = new ReservationBook()
                {
                    IdReservation = reservation.Id,
                    IdBook = book
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
