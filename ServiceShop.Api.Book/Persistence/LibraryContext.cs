﻿using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.Book.Model;

namespace ServiceShop.Api.Book.Persistence
{
    public class LibraryContext:DbContext
    {
        public LibraryContext() { }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public virtual DbSet<MaterialLibrary> MaterialLibrary { get; set; }
    }
}
