using InnoGotchi.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class UserController : ControllerBase
{
    private readonly IIdentityService _identityService;
    public UserController(IIdentityService identityService) => _identityService = identityService;

    [HttpPost("SignIn")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn(UserAuthorizationModel model)
    {
        if (model == null)
            return BadRequest();

        bool result = await _identityService.SignIn(model.Name, model.Password, model.IsPersistent);

        return result ? Ok() : BadRequest();
    }

    [HttpPost("SignOut")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public new async Task<IActionResult> SignOut()
    {
        await _identityService.SignOut();
        return Ok();
    }

    [HttpPost("SignUp")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp(UserAuthorizationModel model)
    {
        if (model == null)
            return BadRequest();

        var (result, userId) = await _identityService.CreateUserAsync(model.Name, model.Password);

        return result.Succeeded ? Created(string.Empty, new { result, userId }) : BadRequest(result.Errors);
    }
}