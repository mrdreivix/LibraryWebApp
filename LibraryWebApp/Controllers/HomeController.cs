using LibraryWebApp.Data;
using LibraryWebApp.Models;
using LibraryWebApp.Models.ViewModels;
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
            IEnumerable<Book> books = _db.Book
                .Include(x => x.BookAuthor)
                .ThenInclude(x => x.Author)
                .Include(x => x.BookType)
                .ThenInclude(x => x.TypeOfBook);
            HomeVM model = new HomeVM
            {
                Books = books,
                BookTypes = _db.BookType
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
