namespace ServiceShop.Api.Book.Model
{
    public class MaterialLibrary
    {
        public Guid? MaterialLibraryId { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Guid? BookAuthor { get; set; }

    }
}
