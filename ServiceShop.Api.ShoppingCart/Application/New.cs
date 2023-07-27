using MediatR;
using ServiceShop.Api.ShoppingCart.Model;
using ServiceShop.Api.ShoppingCart.Persistence;

namespace ServiceShop.Api.ShoppingCart.Application
{
    public class New
    {
        public class Execute : IRequest
        {
            public DateTime? CreationDate { get; set; }
            public List<string> ProductList { get; set; }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly CartContext _context;

            public Handler(CartContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                CartSession cartSession = new()
                {
                    CreationDate = request.CreationDate,
                };

                _context.CartSession.Add(cartSession);
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                {
                    throw new Exception("Error creating session cart");
                }

                int id = cartSession.CartSessionId;

                foreach (var item in request.ProductList)
                {
                    CartSessionDetail detail = new()
                    {
                        SelectedProduct = item,
                        CreationDate = DateTime.Now,
                        CartSessionId = id,
                    };

                    _context.CartSessionDetail.Add(detail);
                }

                result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    throw new Exception("Error inserting shopping cart detail");
                }

                return Unit.Value;
            }
        }
    }
}
