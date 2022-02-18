using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Release Date")]
        public DateTime ReleaseDate { get; set; }
        [DisplayName("Book Authors")]
        public virtual List<BookAuthor> BookAuthor { get; set; }
        [DisplayName("Book Type")]
        public virtual List<BookBookType> BookType { get; set; }
        [DisplayName("Book Comments")]
        public virtual List<BookComment> BookComment { get; set; }
        [DisplayName("Book Rating")]
        public virtual List<BookRating> BookRating { get; set; }
    }
}
