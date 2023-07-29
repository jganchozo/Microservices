using ServiceShop.Api.ShoppingCart.RemoteModel;

namespace ServiceShop.Api.ShoppingCart.RemoteInterface
{
    public interface IBookService
    {
        Task<(bool result, RemoteBook? book, string? errorMessage)> GetRemoteBook(Guid bookId);
    }
}
