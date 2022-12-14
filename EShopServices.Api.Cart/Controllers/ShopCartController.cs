using EShopServices.Api.Cart.Application;
using EShopServices.Api.Cart.Applicationñ;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopServices.Api.Cart.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopCartController : ControllerBase
{
    private readonly IMediator _mediator;
    public ShopCartController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Unit>> Post(New.Execute data)
    {
        return await _mediator.Send(data);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CartDto>> Get(int id)
    {
        return await _mediator.Send(new Get.Execute { CartSessionId = id });
    }
}
