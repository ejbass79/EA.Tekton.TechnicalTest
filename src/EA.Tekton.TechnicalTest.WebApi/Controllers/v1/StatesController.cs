using EA.Tekton.TechnicalTest.WebApi.Requests;
using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Cross.Enums;
using EA.Tekton.TechnicalTest.Cross.Enums.Exts;
using EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Commands;
using EA.Tekton.TechnicalTest.Domain.CQRS.Components.StateComponent.Queries;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EA.Tekton.TechnicalTest.WebApi.Controllers.v1;
    
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class StatesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [ApiVersion("1")]
    [HttpGet("{statusId:int}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResponseService<StateDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseService<StateDto>), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetStatesByStatusIdAsync(int statusId)
    {
        var result = await _mediator.Send(new GetStateByStatusIdQuery(statusId));
        
        var response = new ResponseService<StateDto>
        {
            HttpStatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NoContent,
            Status = result.IsSuccess,
            Message = result.IsSuccess ? CrossEnum.Status.Ok.ToStringAttribute() : CrossEnum.Status.Error.ToStringAttribute(),
            Data = result.Value
        };

        return Ok(response);
    }
    
    [ApiVersion("1")]
    [HttpGet("{page:int}/{limit:int}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResponseService<QueryMultipleResponse<StateDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseService<QueryMultipleResponse<StateDto>>), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetStatesAllAsync(int? page, int? limit)
    {
        var result = await _mediator.Send(new GetStateAllQuery(page ?? 1, limit ?? 1000, "StatusId"));
        
        var response = new ResponseService<QueryMultipleResponse<StateDto>>
        {
            HttpStatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NoContent,
            Status = result.IsSuccess,
            Message = result.IsSuccess ? CrossEnum.Status.Ok.ToStringAttribute() : CrossEnum.Status.Error.ToStringAttribute(),
            Data = result.Value
        };

        return Ok(response);
    }
    
    [ApiVersion("1")]
    [HttpPost]
    [DisableRequestSizeLimit]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResponseService<int>), (int)HttpStatusCode.Created)]
    [Produces(MediaTypeNames.Application.Json, Type = typeof(StateRequest))]
    public async Task<IActionResult> PostStatesAsync([FromBody] StateRequest request)
    {
        var objRequest = request.Adapt<StateDto>();

        objRequest.CreationUser = HttpContext.User.Identity?.Name!;
        objRequest.CreationDate = DateTime.Now;
        objRequest.Deleted = false;

        var status = await _mediator.Send(new CreateStateCommand(objRequest));

        return Ok(new ResponseService<int>
        {
            HttpStatusCode = status.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.InternalServerError,
            Status = status.IsSuccess,
            Data = status.Value.statusId
        });
    }

    [ApiVersion("1")]
    [HttpPut("{statusId:int}")]
    [DisableRequestSizeLimit]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResponseService<bool>), (int)HttpStatusCode.OK)]
    [Produces(MediaTypeNames.Application.Json, Type = typeof(StateRequest))]
    public async Task<IActionResult> PutStatesAsync(int statusId, [FromBody] StateRequest request)
    {
        if (statusId != request.StatusId)
            return BadRequest();

        var objRequest = request.Adapt<StateDto>();
            
        objRequest.ModificationUser = HttpContext.User.Identity?.Name!;
        objRequest.ModificationDate = DateTime.Now;

        var status = await _mediator.Send(new UpdateStateCommand(statusId, objRequest));
        if (status.IsSuccess)
        {
            return Ok(new ResponseService<bool>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Status = true
            });
        }

        var response = new ResponseService<bool>
        {
            HttpStatusCode = HttpStatusCode.NotFound,
            Status = false
        };

        return Ok(response);
    }

    [ApiVersion("1")]
    [HttpDelete("{statusId:int}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResponseService<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteStatesAsync(int statusId)
    {
        var status = await _mediator.Send(new DeleteStateCommand(statusId));
        if (status.IsSuccess)
        {
            return Ok(new ResponseService<bool>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Status = true
            });
        }

        var response = new ResponseService<bool>
        {
            HttpStatusCode = HttpStatusCode.NotFound,
            Status = false
        };

        return Ok(response);
    }

    
        
    
}