using ErrorOr;
using Hakin.Application.Services.Authentication;
using Hakin.Contracts.Authentication;
using Hakin.Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Hakin.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        return authResult.Match(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);

        if (authResult.FirstError == Errors.Authentication.InvalidCredential)
        {
            return Problem(statusCode: StatusCodes.Status403Forbidden, title: authResult.FirstError.Description);
        }

        return authResult.Match(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors)
        );
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult result)
    {
        return new AuthenticationResponse(
            result.User.Id,
            result.User.FirstName,
            result.User.LastName,
            result.User.Email,
            result.Token
        );
    }
}