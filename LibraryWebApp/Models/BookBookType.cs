using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class BookBookType
    {
        [Key]
        public int Id { get; set; }
        public int IdBook { get; set; }
        [ForeignKey("IdBook")]
        public virtual Book Book { get; set; }
        public int IdBookType { get; set; }
        [ForeignKey("IdBookType")]
        public virtual BookType BookType { get; set; }
    }
}
