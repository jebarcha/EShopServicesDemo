using EShopServices.Api.Author.Application;
using EShopServices.Api.Author.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopServices.Api.Author.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
    private readonly IMediator _mediator;
	public AuthorController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost]
	public async Task<ActionResult<Unit>> Create(NewClass.Execute data)
	{
		return await _mediator.Send(data);
	}

	[HttpGet]
	public async Task<ActionResult<List<AuthorDto>>> GetAll()
	{
		return await _mediator.Send(new GetClass.ListAuthor());
	}

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetByFilter(string id)
    {
        return await _mediator.Send(new GetFilterClass.AuthorUnique() { AuthorGuid = id});
    }
}
