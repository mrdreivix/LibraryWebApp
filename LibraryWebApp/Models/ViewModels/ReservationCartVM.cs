using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class ReservationCartVM
    {
        public IEnumerable<Book> books { get; set; }
        public bool alreadyReservedBooks { get; set; }
    }
}
