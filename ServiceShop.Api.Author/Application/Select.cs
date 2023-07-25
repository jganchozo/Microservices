using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.Author.Model;
using ServiceShop.Api.Author.Persistence;

namespace ServiceShop.Api.Author.Application
{
    public class Select
    {
        public class AuthorList : IRequest<List<AuthorDto>>
        {

        }

        public class Handler : IRequestHandler<AuthorList, List<AuthorDto>>
        {
            public readonly AuthorContext _context;
            private readonly IMapper _mapper;

            public Handler(AuthorContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<AuthorDto>> Handle(AuthorList request, CancellationToken cancellationToken)
            {
                var authors = await _context.BookAuthors.ToListAsync();
                var authorsDto = _mapper.Map<List<AuthorDto>>(authors);

                return authorsDto;
            }
        }
    }
}
