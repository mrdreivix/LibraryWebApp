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
            IEnumerable<Book> books = _db.Book.Include(x => x.BookAuthor).ThenInclude(x => x.Author).Include(x => x.BookType).ThenInclude(x => x.TypeOfBook);
            return View(books);
        }
        //GET - CREATE
        public IActionResult Create()
        {
            BookVM model = new BookVM
            {
                Authors = _db.Author,
                Types = _db.TypeOfBook
            };
            return View(model);
        }
        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookVM obj)
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
                if (obj.TypesOfBookId != null)
                {
                    foreach (var p in obj.TypesOfBookId)
                    {
                        BookType bookType = new BookType
                        {
                            IdBook = obj.Book.Id,
                            IdTypeOfBook = p
                        };
                        _db.BookType.Add(bookType);
                    }
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            BookVM model = new BookVM
            {
                Authors = _db.Author,
                Types = _db.TypeOfBook
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
            foreach (var bookType in _db.BookType.Where(x => x.IdBook == id))
            {

                _db.BookType.Remove(bookType);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            BookVM model = new BookVM
            {
                Authors = _db.Author,
                BookAuthors = _db.BookAuthor.Where(x => x.IdBook == id),
                Types = _db.TypeOfBook,
                BookTypes = _db.BookType.Where(x=>x.IdBook == id),
                Book = _db.Book.Include(x => x.BookAuthor).ThenInclude(x => x.Author).Where(x => x.Id == id).Include(x => x.BookType).ThenInclude(x => x.TypeOfBook).Where(x => x.Id == id).FirstOrDefault()
            
            };
            return View(model);
        }
        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookVM obj)
        {
            if (ModelState.IsValid)
            {
                _db.Book.Update(obj.Book);
                _db.SaveChanges();
                
                    foreach (var bookAuthor in _db.BookAuthor.Where(x => x.IdBook == obj.Book.Id))
                    {
                        _db.BookAuthor.Remove(bookAuthor);
                    }
                    foreach (var bookType in _db.BookType.Where(x => x.IdBook == obj.Book.Id))
                    {
                        _db.BookType.Remove(bookType);
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
                if (obj.TypesOfBookId != null)
                {
                    foreach (var typeId in obj.TypesOfBookId)
                    {
                        BookType bookType = new BookType
                        {
                            IdBook = obj.Book.Id,
                            IdTypeOfBook = typeId
                        };
                        _db.BookType.Add(bookType);
                    }
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            BookVM model = new BookVM
            {
                Authors = _db.Author,
                BookAuthors = _db.BookAuthor.Where(x => x.IdBook == obj.Book.Id),
                Types = _db.TypeOfBook,
                BookTypes = _db.BookType.Where(x => x.IdBook == obj.Book.Id),
                Book = _db.Book.Include(x => x.BookAuthor).ThenInclude(x => x.Author).Where(x => x.Id == obj.Book.Id).Include(x => x.BookType).ThenInclude(x => x.TypeOfBook).Where(x => x.Id == obj.Book.Id).FirstOrDefault()
            };
            return View(model);
        }
    }
}
