using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class ReservationCartVM
    {
        public IEnumerable<Book> Books { get; set; }
        public bool AlreadyReservedBooks { get; set; }
    }
}
