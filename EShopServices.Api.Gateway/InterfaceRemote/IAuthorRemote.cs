using EShopServices.Api.Gateway.BookRemote;

namespace EShopServices.Api.Gateway.InterfaceRemote;

public interface IAuthorRemote
{
    Task<(bool Result, AuthorModelRemote Author, string ErrorMessage)> GetAuthor(Guid AuthorId);
}
