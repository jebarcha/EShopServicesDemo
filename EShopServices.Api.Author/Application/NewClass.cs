using EShopServices.Api.Author.Model;
using EShopServices.Api.Author.Persistence;
using FluentValidation;
using MediatR;

namespace EShopServices.Api.Author.Application;

public class NewClass
{
    public class Execute : IRequest 
    {
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? Birthday { get; set; }
    }

    public class ExecuteValidation : AbstractValidator<Execute>
    {
        public ExecuteValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }


    public class Handler : IRequestHandler<Execute>
    {
        public readonly ContextAuthor _context;
        public Handler(ContextAuthor context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
        {
            var authorBook = new AuthorBook
            {
                Name= request.Name,
                LastName = request.LastName, 
                Birthday = request.Birthday,
                AuthorBookGuid = Convert.ToString(Guid.NewGuid())!
            };

            _context.AuthorBook.Add(authorBook);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Unit.Value;
            }

            throw new Exception("Cannot insert the author book");
        }
    }
}
