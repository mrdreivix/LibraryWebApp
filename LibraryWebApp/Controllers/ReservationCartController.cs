using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class ReservationCartController : Controller
    {
        private readonly AppDbContext _db;

        public ReservationCartController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ShoppingCart> shoppingCartList = new List<>();
            if(HttpContext.Session.Get<IEnumerable<ShoppingCart>>(WC.SessionCart))
        }
    }
}
