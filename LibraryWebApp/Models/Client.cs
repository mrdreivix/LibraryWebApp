﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class Client : ApplicationUser
    {
        public virtual List<Reservation> Reservation { get; set; }
    }
}
