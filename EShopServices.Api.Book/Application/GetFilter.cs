using AutoMapper;
using EShopServices.Api.Book.Model;
using EShopServices.Api.Book.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShopServices.Api.Book.Application;

public class GetFilter
{
    public class UniqueBook : IRequest<MaterialBookDto>
    {
        public Guid? BookId { get; set; }
    }

    public class Handler : IRequestHandler<UniqueBook, MaterialBookDto>
    {
        public readonly ContextBookstore _context;
        private readonly IMapper _mapper;

        public Handler(ContextBookstore context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MaterialBookDto> Handle(UniqueBook request, CancellationToken cancellationToken)
        {
            var book = await _context.MaterialLibrary.Where(x => x.MaterialLibraryId == request.BookId).FirstOrDefaultAsync();
            if (book is null)
            {
                throw new Exception("Author not found!");
            }
            var bookDto = _mapper.Map<MaterialBook, MaterialBookDto>(book);
            return bookDto;
        }
    }

}
