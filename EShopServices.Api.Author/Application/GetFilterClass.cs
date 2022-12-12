using AutoMapper;
using EShopServices.Api.Author.Model;
using EShopServices.Api.Author.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShopServices.Api.Author.Application;

public class GetFilterClass
{
    public class AuthorUnique : IRequest<AuthorDto>
    {
        public string AuthorGuid { get; set; }
    }

    public class Handler : IRequestHandler<AuthorUnique, AuthorDto>
    {
        public readonly ContextAuthor _context;
        private readonly IMapper _mapper;

        public Handler(ContextAuthor context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AuthorDto> Handle(AuthorUnique request, CancellationToken cancellationToken)
        {
            var author = await _context.AuthorBook.Where(x => x.AuthorBookGuid== request.AuthorGuid).FirstOrDefaultAsync();
            if (author is null)
            {
                throw new Exception("Author not found!");
            }
            var authorDto = _mapper.Map<AuthorBook, AuthorDto>(author);
            return authorDto;
        }
    }

}
