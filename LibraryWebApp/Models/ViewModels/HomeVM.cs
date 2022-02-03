using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<BookType> BookTypes { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public Book Book { get; set; }
    }
}
