namespace ServiceShop.Api.Gateway.RemoteBook
{
    public class RemoteBookModel
    {
        public Guid? MaterialLibraryId { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Guid? BookAuthor { get; set; }

        public RemoteAuthorModel Author { get; set; }
    }
}
