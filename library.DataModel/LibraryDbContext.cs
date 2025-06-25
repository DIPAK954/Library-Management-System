using library.DataModel.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.DataModel
{
    public class LibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) 
            : base(options)
        {
         
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<IssuedBooks> IssuedBooks { get; set; }
        public DbSet<BookRequest> BookRequests { get; set; }

    }
}
