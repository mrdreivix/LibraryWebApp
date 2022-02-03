using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp
{
    public enum ReservationStatus
    {
        InProgress,
        Taken,
        Returned,
        ReadyToTake,
        Cancelled
    }
}
