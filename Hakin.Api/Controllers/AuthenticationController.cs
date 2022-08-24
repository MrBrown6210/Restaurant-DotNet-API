using ErrorOr;
using Hakin.Application.Authentication.Commands.Register;
using Hakin.Application.Authentication.Common;
using Hakin.Contracts.Authentication;
using Hakin.Domain.Common.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hakin.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        var authResult = await _mediator.Send(command);

        return authResult.Match(
            result => Ok(MapAuthResult(result)),
            errors => Problem(errors)
        );
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(
            request.Email,
            request.Password
        );
        var authResult = await _mediator.Send(query);

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