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
using LibraryWebApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> Index()
        {
            var reservationCartEntries = new List<ReservationCartEntry>();
            if(HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart) !=null
                && HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart).Count() > 0)
            {
                reservationCartEntries = HttpContext.Session.Get<List<ReservationCartEntry>>(WC.SessionCart);
            }
            var userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var reservationCartVM = new ReservationCartVM();
            var booksIdsInCart = reservationCartEntries.Select(i => i.BookIdInCart).ToList();
            if (_db.Reservation.Where(x => x.DateOfReturn < x.DateOfReservation && x.IdClient == user.Id && x.ReservationBook
            .Where(y => booksIdsInCart
            .Contains(y.IdBook))
            .Count() > 0)
            .Include(x => x.ReservationBook)
            .Count() > 0)
            {
                var reservedBooks = _db.Reservation.Where(x => x.DateOfReturn < x.DateOfReservation && x.IdClient == user.Id).Include(x => x.ReservationBook).ToList();
                reservationCartVM.AlreadyReservedBooks = true;
                foreach (Reservation reservation in reservedBooks)
                {
                    foreach (ReservationBook reservationBook in reservation.ReservationBook)
                    {
                        reservationCartEntries.Remove(reservationCartEntries.FirstOrDefault(u => u.BookIdInCart == reservationBook.IdBook));
                        HttpContext.Session.Set(WC.SessionCart, reservationCartEntries);
                    }
                }
            }
            booksIdsInCart = reservationCartEntries.Select(i => i.BookIdInCart).ToList();
            reservationCartVM.Books = _db.Book.Where(u => booksIdsInCart.Contains(u.Id));
            return View(reservationCartVM);
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
                DateOfReservation = DateTime.Today,
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
