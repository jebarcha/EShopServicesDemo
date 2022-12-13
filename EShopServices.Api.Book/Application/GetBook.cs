using AutoMapper;
using EShopServices.Api.Book.Model;
using EShopServices.Api.Book.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EShopServices.Api.Book.Application;

public class GetBook
{
    public class Execute: IRequest<List<MaterialBookDto>> {  }

    public class Handler : IRequestHandler<Execute, List<MaterialBookDto>>
    {
        private readonly ContextBookstore _context;
        private readonly IMapper _mapper;

        public Handler(ContextBookstore context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MaterialBookDto>> Handle(Execute request, CancellationToken cancellationToken)
        {
            var books = await _context.MaterialLibrary.ToListAsync();
            var booksDto = _mapper.Map<List<MaterialBook>, List<MaterialBookDto>>(books);
            return booksDto;
        }
    }

}
