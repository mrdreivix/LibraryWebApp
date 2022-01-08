using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AppDbContext _db;

        public AuthorController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Author> objList = _db.Author;
            return View(objList);
        }

        //GET - CREATE
        public IActionResult Create()
        {

            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author obj)
        {
           

            if (ModelState.IsValid)
            {
                _db.Author.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Author.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author obj)
        {
            if (ModelState.IsValid)
            {
                _db.Author.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Author.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Author.Remove(obj);
            foreach (var k in _db.BookAuthor.Where(x => x.IdAuthor == id))
            {
                _db.BookAuthor.Remove(k);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
