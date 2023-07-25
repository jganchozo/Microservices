using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.Author.Model;

namespace ServiceShop.Api.Author.Persistence
{
    public class AuthorContext: DbContext
    {
        public AuthorContext(DbContextOptions<AuthorContext> options): base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Degrees> Degrees { get; set; }
    }
}
