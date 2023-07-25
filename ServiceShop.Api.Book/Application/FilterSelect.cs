using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.Book.Persistence;

namespace ServiceShop.Api.Book.Application
{
    public class FilterSelect
    {
        public class Execute : IRequest<MaterialLibraryDto> 
        {
            public Guid BookId { get; set; }
        }

        public class Handler : IRequestHandler<Execute, MaterialLibraryDto>
        {
            private readonly LibraryContext _context;
            private readonly IMapper _mapper;

            public Handler(LibraryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<MaterialLibraryDto> Handle(Execute request, CancellationToken cancellationToken)
            {
                var book = await _context.MaterialLibrary.Where(x => x.MaterialLibraryId == request.BookId).FirstOrDefaultAsync();

                if (book is null)
                {
                    throw new Exception("Book not found");
                }

                var bookDto = _mapper.Map<MaterialLibraryDto>(book);

                return bookDto;
            }
        }
    }
}
