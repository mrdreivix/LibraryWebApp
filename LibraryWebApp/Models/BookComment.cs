using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class BookComment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime DateOfCommentCreation { get; set; }
        public int IdBook { get; set; }
        [ForeignKey("IdBook")]
        public virtual Book Book { get; set; }
        public string IdClient { get; set; }
        [ForeignKey("IdClient")]
        public virtual Client Client { get; set; }

    }
}
