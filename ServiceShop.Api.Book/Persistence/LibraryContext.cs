using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.Book.Model;

namespace ServiceShop.Api.Book.Persistence
{
    public class LibraryContext:DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }

        public DbSet<MaterialLibrary> MaterialLibrary { get; set; }
    }
}
