namespace ServiceShop.Api.ShoppingCart.Model
{
    public class CartSession
    {
        public int CartSessionId { get; set; }
        public DateTime? CreationDate { get; set; }
        public ICollection<CartSessionDetail> DetailList { get; set; }
    }
}
