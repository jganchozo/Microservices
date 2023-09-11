using FluentValidation;
using MediatR;
using ServiceShop.Api.Book.Model;
using ServiceShop.Api.Book.Persistence;
using ServiceShop.RabbitMQ.Bus.Bus;
using ServiceShop.RabbitMQ.Bus.QueueEvent;

namespace ServiceShop.Api.Book.Application
{
    public class New
    {
        public class Execute: IRequest<Unit>
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

        public class Handler : IRequestHandler<Execute, Unit>
        {
            private readonly LibraryContext _context;
            private readonly IRabbitEventBus _eventBus;

            public Handler(LibraryContext context, IRabbitEventBus eventBus)
            {
                _context = context;
                _eventBus = eventBus;
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

                _eventBus.Publish(new EmailEventQueue("joseganchozo@outlook.com", request.Title, "Example body"));

                return Unit.Value;
            }

            //public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            //{
            //    MaterialLibrary book = new()
            //    {
            //        Title = request.Title,
            //        PublicationDate = request.PublicationDate,
            //        BookAuthor = request.BookAuthor,
            //    };

            //    _context.MaterialLibrary.Add(book);
            //    var result = await _context.SaveChangesAsync();

            //    if (result <= 0)
            //    {
            //        throw new Exception("Error inserting book");
            //    }

            //    _eventBus.Publish(new EmailQueueEvent("jganchozo@outlook.com", request.Title, "Example content"));

            //    return Unit.Value;
            //}
        }
    }
}
