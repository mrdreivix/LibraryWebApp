using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class DetailsVM
    {
        public Book Book { get; set; }
        public bool ExistsInCart { get; set; }
        public IEnumerable<Reservation> Reservations{ get; set; }

    }
}
