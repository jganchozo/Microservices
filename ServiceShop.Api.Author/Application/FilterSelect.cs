using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceShop.Api.Author.Model;
using ServiceShop.Api.Author.Persistence;

namespace ServiceShop.Api.Author.Application
{
    public class FilterSelect
    {
        public class UniqueAuthor: IRequest<AuthorDto>
        {
            public string BookAuthorGuid { get; set; }
        }

        public class Handler : IRequestHandler<UniqueAuthor, AuthorDto>
        {
            public readonly AuthorContext _context;
            public readonly IMapper _mapper;

            public Handler(AuthorContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<AuthorDto> Handle(UniqueAuthor request, CancellationToken cancellationToken)
            {
                var author = await _context.BookAuthors.Where(x => x.BookAuthorGuid == request.BookAuthorGuid).SingleOrDefaultAsync();

                if (author is null)
                {
                    throw new Exception("Author not found");
                }

                var authorDto = _mapper.Map<AuthorDto>(author);

                return authorDto;
            }
        }
    }
}
