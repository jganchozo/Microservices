using FluentValidation;
using MediatR;
using ServiceShop.Api.Author.Model;
using ServiceShop.Api.Author.Persistence;

namespace ServiceShop.Api.Author.Application
{
    public class New
    {
        public class Execute : IRequest<Unit>
        {
            public string Name { get; set; }
            public string LastName { get; set; }
            public DateTime? DateOfBirth { get; set; }
        }

        public class ExecuteValidations: AbstractValidator<Execute>
        {
            public ExecuteValidations()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute, Unit>
        {
            public readonly AuthorContext _context;

            public Handler(AuthorContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                BookAuthor bookAuthor = new()
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    DateOfBirth = request.DateOfBirth,
                    BookAuthorGuid = Guid.NewGuid().ToString(),
                };

                _context.BookAuthors.Add(bookAuthor);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    throw new Exception("Error inserting Author");
                }

                return Unit.Value;
            }
        }
    }
}
