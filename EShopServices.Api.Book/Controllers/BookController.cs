using EShopServices.Api.Book.Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopServices.Api.Book.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly IMediator _mediator;
    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> Post(NewBook.Execute data)
    {
        return await _mediator.Send(data);
    }

    [HttpGet]
    public async Task<ActionResult<List<MaterialBookDto>>> GetAll()
    {
        return await _mediator.Send(new GetBook.Execute());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MaterialBookDto>> Get(Guid id)
    {
        return await _mediator.Send(new GetFilter.UniqueBook { BookId = id });
    }
}
