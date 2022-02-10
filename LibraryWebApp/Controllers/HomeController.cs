using LibraryWebApp.Data;
using LibraryWebApp.Models;
using LibraryWebApp.Models.ViewModels;
using LibraryWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(ILogger<HomeController> logger, AppDbContext db, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            HomeVM model = new HomeVM
            {
                Books = _db.Book.Include(x => x.BookAuthor)
                .ThenInclude(x => x.Author)
                .Include(x=>x.BookRating)
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

        public async Task<IActionResult> Details(int? id)
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
                  .Include(x=>x.BookComment)
                  .ThenInclude(x=>x.Client)
                  .Include(x => x.BookType)
                  .ThenInclude(x => x.BookType)
                  .Where(x => x.Id == id)
                  .FirstOrDefault(),
                Reservations = _db.Reservation.Include(x => x.ReservationBook),
                ExistsInCart = false
            };
            if (User.IsInRole(WC.CustomerRole))
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                if (_db.BookRating.Any(x => x.IdClient == user.Id && x.IdBook == id))
                {
                    model.LoggedUserRating = _db.BookRating.Where(x => x.IdClient == user.Id && x.IdBook == id).FirstOrDefault().Rate;
                }
            }
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
        [Authorize(Roles = WC.CustomerRole)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(BookComment bookComment)
        {
            var model = _db.Book.Find(bookComment.IdBook);
            if (ModelState.IsValid)
            {
                var userName = HttpContext.User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                bookComment.IdClient = user.Id;
                bookComment.DateOfCommentCreation = DateTime.Now;
                _db.BookComment.Add(bookComment);
                _db.SaveChanges();
            }
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
        [Authorize(Roles = WC.AdminRole + "," + WC.WorkerRole + "," + WC.CustomerRole)]
        public IActionResult Delete(int id)
        {
            var bookComment = _db.BookComment.Find(id);
            _db.BookComment.Remove(bookComment);
            _db.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = bookComment.IdBook });
        }
        [Authorize(Roles = WC.CustomerRole)]
        public async Task<IActionResult> RateBook(int id,int rate)
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if(rate == 0)
            {
                var bookRating = _db.BookRating.Where(x => x.IdClient == user.Id && x.IdBook == id).FirstOrDefault();
                _db.BookRating.Remove(bookRating);
            }
            else if (_db.BookRating.Any(x => x.IdClient == user.Id && x.IdBook == id))
            {
                var bookRating = _db.BookRating.Where(x => x.IdClient == user.Id && x.IdBook == id).FirstOrDefault();
                bookRating.Rate = rate;
                _db.BookRating.Update(bookRating);
            }
            else
            {
                var bookRating = new BookRating()
                {
                    IdBook = id,
                    Rate = rate,
                    IdClient = user.Id,
                };

                _db.BookRating.Add(bookRating);
            }
            _db.SaveChanges();
            return View(id);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
