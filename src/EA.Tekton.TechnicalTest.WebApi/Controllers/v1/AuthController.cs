using EA.Tekton.TechnicalTest.Cross.Auth.CQRS.Components.AuthComponent.Commands;
using EA.Tekton.TechnicalTest.Cross.Auth.Models;
using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Cross.Helpers;
using EA.Tekton.TechnicalTest.WebApi.Errors;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Net.Mime;

using LoginRequest = EA.Tekton.TechnicalTest.WebApi.Requests.LoginRequest;
using RegisterRequest = EA.Tekton.TechnicalTest.WebApi.Requests.RegisterRequest;

namespace EA.Tekton.TechnicalTest.WebApi.Controllers.v1;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [ApiVersion("1")]
    [HttpPost("login")]
    [Produces(MediaTypeNames.Application.Json, Type = typeof(LoginRequest))]
    [ProducesResponseType(typeof(ResponseService<AuthToken?>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseService<AuthToken?>), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
    {
        if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
            return BadRequest(new ResponseService<IActionResult>
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Status = false,
                Message = $"{WebApiErrors.NotFound} / {WebApiErrors.FailedLoading} / {WebApiErrors.FailedDecrypting}"
            });
        }

        var loginQuery = loginRequest.Adapt<LoginCommand>();

        var result = await _mediator.Send(loginQuery);

        return Ok(new ResponseService<AuthToken?>
        {
            HttpStatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NoContent,
            Status = result.IsSuccess,
            Data = result!.Value
        });
    }

    [ApiVersion("1")]
    [HttpPost("register")]
    [Produces(MediaTypeNames.Application.Json, Type = typeof(RegisterRequest))]
    [ProducesResponseType(typeof(ResponseService<IActionResult>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Password))
        {
            request.Password = FunHelper.GeneratePassword(34);
        }

        var registerUserCommand = request.Adapt<RegisterUserCommand>();

        registerUserCommand.Token = Ulid.NewUlid().ToBase64();

        var response = await _mediator.Send(registerUserCommand);

        return Ok(new ResponseService<IActionResult>
        {
            HttpStatusCode = HttpStatusCode.OK,
            Status = response.IsSuccess,
            Message = response.IsSuccess ? HttpStatusCode.OK.ToString() : response.Error.Name
        });
    }
}