using EShopServices.Api.Cart.RemoteModel;

namespace EShopServices.Api.Cart.RemoteInterface;

public interface IBookService
{
    Task<(bool result, RemoteBook book, string ErrorMessage)> GetBook(Guid id);
}
