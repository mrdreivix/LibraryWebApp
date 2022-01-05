﻿using System;
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
        public string Description { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public string Author { get; set; }
        public string TypeOfBook { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<BookAuthor> BookAuthor { get; set; }
    }
}
