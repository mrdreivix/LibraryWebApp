using LibraryWebApp.Data;
using LibraryWebApp.Models;
using LibraryWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class FeeController : Controller
    {
        private readonly AppDbContext _db;

        
        public FeeController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var FeeVM = new FeeVM()
            {
                FeesList = _db.Fee.Where(x => x.IdReservation == id),
                IdReservation = id
            };
            return View(FeeVM);
        }

        //GET - CREATE
        public IActionResult Create(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var fee = new Fee()
            {
                IdReservation = id,
            };
            return View(fee);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Fee fee)
        {
            fee.Id = 0;
            if (ModelState.IsValid)
            {
                _db.Fee.Add(fee);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { id = fee.IdReservation });
            }
            return View(fee);
        }

        //GET - EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fee = _db.Fee.Find(id);
            if (fee == null)
            {
                return NotFound();
            }
            return View(fee);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Fee fee)
        {
            if (ModelState.IsValid)
            {
                _db.Fee.Update(fee);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index), new { id = fee.IdReservation });
            }
            return View(fee);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var fee = _db.Fee.Find(id);
            if (fee == null)
            {
                return NotFound();
            }
            _db.Fee.Remove(fee);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index), new { id = fee.IdReservation });
        }
    }
    
}
