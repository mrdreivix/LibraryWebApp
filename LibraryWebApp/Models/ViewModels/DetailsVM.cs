using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Models.ViewModels
{
    public class DetailsVM
    {
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<BookAuthor> BookAuthors { get; set; }
        public Book Book { get; set; }
        public List<int> AuthorsId { get; set; }
        public IEnumerable<BookBookType> BookTypes { get; set; }
        public IEnumerable<BookType> Types { get; set; }
        public List<int> BookTypesId { get; set; }
        public bool ExistsInCart { get; set; }
    }
}
