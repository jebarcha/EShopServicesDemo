using AutoMapper;
using EShopServices.Api.Author.Model;
using EShopServices.Api.Author.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShopServices.Api.Author.Application;

public class GetClass
{
    public class ListAuthor : IRequest<List<AuthorDto>>
    {
    }

    public class Handler : IRequestHandler<ListAuthor, List<AuthorDto>>
    {
        private readonly ContextAuthor _context;
        private readonly IMapper _mapper;

        public Handler(ContextAuthor context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AuthorDto>> Handle(ListAuthor request, CancellationToken cancellationToken)
        {
            var authors = await _context.AuthorBook.ToListAsync();
            var authorsDto = _mapper.Map<List<AuthorBook>, List<AuthorDto>>(authors);
            return authorsDto;
        }
    }

}
