using LibraryWebApp.Data;
using LibraryWebApp.Models;
using LibraryWebApp.Models.ViewModels;
using LibraryWebApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;
        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM
            {
                Books = _db.Book.Include(x => x.BookAuthor)
                .ThenInclude(x => x.Author)
                .Include(x => x.BookType)
                .ThenInclude(x => x.BookType),
                BookTypes = _db.BookType,
                Reservations = _db.Reservation.Include(x => x.ReservationBook),
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            List<ReservationCartEntry> shoppingCartList = new List<ReservationCartEntry>();
            if (HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart) != null
                && HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ReservationCartEntry>>(WC.SessionCart);
            }
            var model = new DetailsVM()
            {
                Book = _db.Book
                  .Include(x => x.BookAuthor)
                  .ThenInclude(x => x.Author)
                  .Include(x => x.BookType)
                  .ThenInclude(x => x.BookType)
                  .Where(x=>x.Id==id)
                  .FirstOrDefault(),
                Reservations = _db.Reservation.Include(x=>x.ReservationBook),
                ExistsInCart = false
            };
            foreach(var item in shoppingCartList)
            {
                if(item.BookIdInCart == id)
                {
                    model.ExistsInCart = true;
                }
            }
            return View(model);
        }
        [HttpPost,ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ReservationCartEntry> shoppingCartList = new List<ReservationCartEntry>();
            if(HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart)!=null 
                && HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart).Count() > 0)
            {
                shoppingCartList = HttpContext.Session.Get<List<ReservationCartEntry>>(WC.SessionCart);
            }
            shoppingCartList.Add(new ReservationCartEntry { BookIdInCart = id });
            HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
    public IActionResult RemoveFromCart(int id)
    {
        List<ReservationCartEntry> shoppingCartList = new List<ReservationCartEntry>();
        if (HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart) != null
            && HttpContext.Session.Get<IEnumerable<ReservationCartEntry>>(WC.SessionCart).Count() > 0)
        {
            shoppingCartList = HttpContext.Session.Get<List<ReservationCartEntry>>(WC.SessionCart);
        }

            var itemToRemove = shoppingCartList.SingleOrDefault(r => r.BookIdInCart == id);
            if (itemToRemove != null)
            {
                shoppingCartList.Remove(itemToRemove);
            }
        HttpContext.Session.Set(WC.SessionCart, shoppingCartList);
        return RedirectToAction(nameof(Index));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
