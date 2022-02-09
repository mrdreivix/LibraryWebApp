using LibraryWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<BookBookType> BookBookType { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<BookType> BookType { get; set; }
        public DbSet<Fee> Fee { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<ReservationBook> ReservationBook { get; set; }
        public DbSet<BookComment> BookComment { get; set; }
        public DbSet<BookRating> BookRating { get; set; }
    }
}
