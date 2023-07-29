using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.ShoppingCart.Persistence;
using ServiceShop.Api.ShoppingCart.RemoteInterface;

namespace ServiceShop.Api.ShoppingCart.Application
{
    public class Select
    {
        public class Execute : IRequest<CartDto>
        {
            public int CartSessionId { get; set; }
        }

        public class Handler : IRequestHandler<Execute, CartDto>
        {
            private readonly CartContext _cartContext;
            private readonly IBookService _bookService;

            public Handler(CartContext cartContext, IBookService bookService)
            {
                _cartContext = cartContext;
                _bookService = bookService;
            }

            public async Task<CartDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var cartSession = await _cartContext.CartSession.FirstOrDefaultAsync(x => x.CartSessionId == request.CartSessionId);
                var cartSessionDetail = await _cartContext.CartSessionDetail.Where(x => x.CartSessionId == request.CartSessionId).ToListAsync();

                List<CartDetailDto> ProductList = new();

                foreach (var book in cartSessionDetail)
                {
                    var response = await _bookService.GetRemoteBook(new Guid(book.SelectedProduct));

                    if (response.result)
                    {
                        var bookResult = response.book;

                        CartDetailDto cartDetail = new()
                        {
                            BookTitle = bookResult.Title,
                            PublicationDate = bookResult.PublicationDate,
                            BookId = bookResult.MaterialLibraryId.Value,
                        };

                        ProductList.Add(cartDetail);
                    }
                }

                CartDto cartDto = new() 
                {
                    ProductList = ProductList,
                    CartId = cartSession.CartSessionId,
                    CreationDate = cartSession.CreationDate
                };

                return cartDto;

            }
        }
    }
}
