﻿namespace ServiceShop.Api.Book.Application
{
    public class MaterialLibraryDto
    {
        public Guid? MaterialLibraryId { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Guid? BookAuthor { get; set; }
    }
}
