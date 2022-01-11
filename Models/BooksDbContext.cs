using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBooks.Models
{
    public class BooksDbContext: DbContext
    {
        public DbSet<Books> Books { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options)
        {

        }
    }
}
