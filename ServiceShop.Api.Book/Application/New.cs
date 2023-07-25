using FluentValidation;
using MediatR;
using ServiceShop.Api.Book.Model;
using ServiceShop.Api.Book.Persistence;

namespace ServiceShop.Api.Book.Application
{
    public class New
    {
        public class Execute: IRequest
        {
            public string Title { get; set; }
            public DateTime? PublicationDate { get; set; }
            public Guid? BookAuthor { get; set; }
        }

        public class ExecuteValidations:AbstractValidator<Execute>
        {
            public ExecuteValidations()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.PublicationDate).NotEmpty();
                RuleFor(x => x.BookAuthor).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly LibraryContext _context;

            public Handler(LibraryContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                MaterialLibrary book = new()
                {
                    Title = request.Title,
                    PublicationDate = request.PublicationDate,
                    BookAuthor = request.BookAuthor,
                };

                _context.MaterialLibrary.Add(book);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    throw new Exception("Error inserting book");
                }

                return Unit.Value;
            }
        }
    }
}
