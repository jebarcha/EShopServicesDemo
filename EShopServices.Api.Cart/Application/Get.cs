using EShopServices.Api.Cart.Persistence;
using EShopServices.Api.Cart.RemoteInterface;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EShopServices.Api.Cart.Application;

public class Get
{
    public class Execute: IRequest<CartDto>
    {
        public int CartSessionId { get; set; }

    }

    public class Hander : IRequestHandler<Execute, CartDto>
    {
        private readonly CartContext _context;
        private readonly IBookService _bookService;

        public Hander(CartContext context, IBookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        public async Task<CartDto> Handle(Execute request, CancellationToken cancellationToken)
        {
            var cartSession = _context.CartSession.FirstOrDefault(x => x.CartSessionId == request.CartSessionId);
            var cartSessionDetail = _context.CartSessionDetail.Where(x => x.CartSessionId == request.CartSessionId);

            var cartDtoList = new List<CartDetailDto>();

            foreach (var book in cartSessionDetail)
            {
                var response = await _bookService.GetBook(new Guid(book.SelectedProduct));
                if (response.result)
                {
                    var bookObj = response.book;
                    var bookDetail = new CartDetailDto
                    {
                        Title = bookObj.Title,
                        PublishDate = bookObj.PublishDate,
                        BookId= bookObj.MaterialLibraryId
                    };
                    cartDtoList.Add(bookDetail);
                }
            }

            var cartSessionDto = new CartDto
            {
                CartId = cartSession.CartSessionId,
                CreationDate = cartSession.CreationDate,
                ProductList= cartDtoList
            };

            return cartSessionDto;

        }
    }

}
