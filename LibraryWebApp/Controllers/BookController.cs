using LibraryWebApp.Data;
using LibraryWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _db;

        public BookController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Book> books = _db.Book.Include(x => x.BookAuthor).ThenInclude(x => x.Author);
            return View(books);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            BookAuthorVM model = new BookAuthorVM
            {
                Authors = _db.Author
            };
            return View(model);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookAuthorVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Add(obj.Book);
                _db.SaveChanges();
                if (obj.AuthorsId != null)
                {
                    foreach (var p in obj.AuthorsId)
                    {
                        BookAuthor bookAuthor = new BookAuthor
                        {
                            IdBook = obj.Book.Id,
                            IdAuthor = p
                        };
                        _db.BookAuthor.Add(bookAuthor);
                    }
                }
                _db.SaveChanges();
               
                return RedirectToAction("Index");
            }
            BookAuthorVM model = new BookAuthorVM
            {
                Authors = _db.Author
            };
            return View(model);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Book.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Book.Remove(obj);

            foreach (var bookAuthor in _db.BookAuthor.Where(x => x.IdBook == id))
            {
                
                _db.BookAuthor.Remove(bookAuthor);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            BookAuthorVM model = new BookAuthorVM
            {
                Authors = _db.Author,
                BookAuthors = _db.BookAuthor.Where(x => x.IdBook == id),
                Book = _db.Book.Include(x => x.BookAuthor).ThenInclude(x => x.Author).Where(x => x.Id == id).FirstOrDefault()
            };
            return View(model);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookAuthorVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Update(obj.Book);
                _db.SaveChanges();
                
                    foreach (var bookAuthor in _db.BookAuthor.Where(x => x.IdBook == obj.Book.Id))
                    {
                        _db.BookAuthor.Remove(bookAuthor);
                    }
                if (obj.AuthorsId != null)
                {
                    foreach (var authorId in obj.AuthorsId)
                    {
                        BookAuthor bookAuthor = new BookAuthor
                        {
                            IdBook = obj.Book.Id,
                            IdAuthor = authorId
                        };
                        _db.BookAuthor.Add(bookAuthor);
                    }
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            BookAuthorVM model = new BookAuthorVM
            {
                Authors = _db.Author,
                BookAuthors = _db.BookAuthor.Where(x => x.IdBook == obj.Book.Id),
                Book = _db.Book.Include(x => x.BookAuthor).ThenInclude(x => x.Author).Where(x => x.Id == obj.Book.Id).FirstOrDefault()
            };
            return View(model);
        }
    }
}
