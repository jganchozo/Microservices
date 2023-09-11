using ServiceShop.Api.Gateway.RemoteBook;

namespace ServiceShop.Api.Gateway.RemoteInterface
{
    public interface IRemoteAuthor
    {
        Task<(bool result, RemoteAuthorModel author, string errorMessage)> GetAuthor(Guid AuthorId);
    }
}
