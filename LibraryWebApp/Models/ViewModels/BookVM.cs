using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models
{
    public class BookVM
    {
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<BookAuthor> BookAuthors { get; set; }
        public Book Book { get; set; }
        public List<int> AuthorsId { get; set; }
        public IEnumerable<BookBookType> BookTypes { get; set; }
        public IEnumerable<BookType> Types { get; set; }
        public List<int> BookTypesId { get; set; }
    }
}
