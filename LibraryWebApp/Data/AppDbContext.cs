using LibraryWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookAuthor>()
                .HasOne(b => b.Book)
                .WithMany(ba => ba.BookAuthor)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<BookAuthor>()
               .HasOne(b => b.Author)
               .WithMany(ba => ba.BookAuthor)
               .HasForeignKey(bi => bi.AuthorId);
        }

        public DbSet<TypeOfBook> TypeOfBook { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Fee> Fee { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
    }
}
