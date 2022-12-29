using EShopServices.Api.Book.Model;
using EShopServices.Api.Book.Persistence;
using EShopServices.RabbitMQ.Bus.BusRabbit;
using EShopServices.RabbitMQ.Bus.EventQueue;
using FluentValidation;
using MediatR;

namespace EShopServices.Api.Book.Application;

public class NewBook
{
    public class Execute: IRequest
    {
        public string Title { get; set; } = null!;
        public DateTime? PublishDate { get; set; }
        public Guid? AuthorBook { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<Execute>
    {
        public ExecuteValidation()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.PublishDate).NotEmpty();
            RuleFor(x => x.AuthorBook).NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Execute>
    {
        public readonly ContextBookstore _context;
        public readonly IRabbitEventBus _eventBus;
        public Handler(ContextBookstore context, IRabbitEventBus eventBus)
        {
            _context = context;
            _eventBus = eventBus;
        }

        public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
        {
            var book = new Model.MaterialBook
            {
                Title = request.Title,
                PublishDate= request.PublishDate,
                AuthorBookId= request.AuthorBook
            };

            _context.MaterialLibrary.Add(book);

            var result = await _context.SaveChangesAsync();

            _eventBus.Publish(new EmailEventQueue("jebarcha@hotmail.com", request.Title, "This is a demo"));

            if (result > 0)
            {
                return Unit.Value;
            }

            throw new Exception("Cannot insert the book");
        }
    }
}
