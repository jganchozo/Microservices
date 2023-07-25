using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.Book.Persistence;

namespace ServiceShop.Api.Book.Application
{
    public class SelectAll
    {
        public class Execute : IRequest<List<MaterialLibraryDto>> { }

        public class Handler : IRequestHandler<Execute, List<MaterialLibraryDto>>
        {
            private readonly LibraryContext _context;
            private readonly IMapper _mapper;

            public Handler(LibraryContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<MaterialLibraryDto>> Handle(Execute request, CancellationToken cancellationToken)
            {
                var books = await _context.MaterialLibrary.ToListAsync();
                var booksDto = _mapper.Map<List<MaterialLibraryDto>>(books);

                return booksDto;
            }
        }
    }
}
