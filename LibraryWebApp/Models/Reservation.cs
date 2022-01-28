using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Date Of Reservation")]
        public DateTime DateOfReservation { get; set; }
        [DisplayName("Planned Date Of Return")]
        public DateTime PlannedDateOfReturn { get; set; }
        [DisplayName("Date Of Return")]
        public DateTime DateOfReturn { get; set; }
        public string IdClient { get; set; }
        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }
        [DisplayName("Reserved Books")]
        public virtual List<ReservationBook> ReservationBook { get; set; }
        public virtual List<Fee> Fee { get; set; }

    }
}
