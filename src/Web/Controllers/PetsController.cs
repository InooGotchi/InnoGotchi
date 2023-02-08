using InnoGotchi.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Controllers.Base;

namespace Microsoft.Extensions.DependencyInjection.Controllers;

public sealed class PetsController : ApiController
{
    private readonly IPetService _service;

    public PetsController(IPetService service)
    {
        _service = service;
    }

    [HttpGet("id")]
    public async 
        //GetByID Update Delete
}