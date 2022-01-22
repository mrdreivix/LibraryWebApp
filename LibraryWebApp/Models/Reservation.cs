using System;
using System.Collections.Generic;
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
        public DateTime DateOfReservation { get; set; }
        public DateTime PlannedDateOfReturn { get; set; }
        public DateTime DateOfReturn { get; set; }
        public string IdClient { get; set; }
        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }
        public virtual List<ReservationBook> ReservationBook { get; set; }
    }
}
