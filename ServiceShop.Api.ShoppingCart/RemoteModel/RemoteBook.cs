namespace ServiceShop.Api.ShoppingCart.RemoteModel
{
    public class RemoteBook
    {
        public Guid? MaterialLibraryId { get; set; }
        public string Title { get; set; }
        public DateTime? PublicationDate { get; set; }
        public Guid? BookAuthor { get; set; }
    }
}
