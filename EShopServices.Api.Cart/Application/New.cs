using EShopServices.Api.Cart.Model;
using EShopServices.Api.Cart.Persistence;
using MediatR;

namespace EShopServices.Api.Cart.Application
{
    public class New
    {
        public class Execute : IRequest
        {
            public DateTime SessionCreationDate { get; set; }
            public List<string> ProductList { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            public readonly CartContext _context;
            public Handler(CartContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var cartSession = new CartSession 
                {
                    CreationDate = request.SessionCreationDate
                };

                _context.CartSession.Add(cartSession);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("Cannot insert the cart session");
                }

                int id = cartSession.CartSessionId;

                foreach (var item in request.ProductList)
                {
                    var sessionDetail = new CartSessionDetail
                    {
                        CartSessionId = id,
                        SelectedProduct = item,
                        CreationDate = DateTime.UtcNow
                    };

                    _context.CartSessionDetail.Add(sessionDetail);
                }

                result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Cannot insert the cart session detail");
            }
        }

    }
}
