using CleanArchitecture.Application.Common.Interfaces;
using InnoGotchi.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Controllers.Base;
using Web.Models;

namespace Web.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public class UserController : ApiController

{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;
    public UserController(IIdentityService identityService, ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    [HttpPost("SignIn")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignIn(UserAuthenticationModel model)
    {
        if (model == null)
            return BadRequest();

        var result = await _identityService.SignIn(model.Name, model.Password, model.IsPersistent);

        if (!result)
            return BadRequest("Invalid username or password");


        var (userId, userName) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(model.Name));

        var token = _tokenService.GenerateJWTToken((userId, userName));

        return Ok(new AuthenticationResponseDTO() { UserId = userId, Name = userName, Token = token });
    }

    [HttpPost("SignUp")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp(UserAuthenticationModel model)
    {
        if (model == null)
            return BadRequest();

        var (result, userId) = await _identityService.CreateUserAsync(model.Name, model.Password);

        return result.Succeeded ? Created(string.Empty, new { result, userId }) : BadRequest(result.Errors);
    }
}