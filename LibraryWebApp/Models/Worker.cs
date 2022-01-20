using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class Worker : ApplicationUser
    {
        public decimal Earnings { get; set; }
    }
}
