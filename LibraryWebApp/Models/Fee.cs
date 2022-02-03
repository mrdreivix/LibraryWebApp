using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class Fee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Paid { get; set; }
        [Required]
        [DisplayName("Amount to Pay")]
        public decimal AmountToPay { get; set; }
        public int IdReservation { get; set; }
        [ForeignKey("IdReservation")]
        public virtual Reservation Reservation { get; set; }
    }
}
