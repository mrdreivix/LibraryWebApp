using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
        public DateTime ReleaseDate { get; set; }
        public virtual List<BookAuthor> BookAuthor { get; set; }
        public virtual List<BookType> BookType { get; set; }
    }
}
