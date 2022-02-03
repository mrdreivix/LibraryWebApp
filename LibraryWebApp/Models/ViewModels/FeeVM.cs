using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class FeeVM
    {
        public IEnumerable<Fee> FeesList { get; set; }
        public int IdReservation { get; set; }
    }
}
