using LibraryWebApp.Data;
using LibraryWebApp.Models;
using LibraryWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{ 
    [Authorize]
    public class ReservationCartController : Controller
    {
        private readonly AppDbContext _db;

        public ReservationCartController(AppDbContext db)
        {
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

        
    }
}
