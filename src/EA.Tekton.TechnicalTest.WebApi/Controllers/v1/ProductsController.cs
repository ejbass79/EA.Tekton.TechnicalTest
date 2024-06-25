using EA.Tekton.TechnicalTest.WebApi.Requests;
using EA.Tekton.TechnicalTest.Cross.Dto;
using EA.Tekton.TechnicalTest.Cross.Enums;
using EA.Tekton.TechnicalTest.Cross.Enums.Exts;
using EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Commands;
using EA.Tekton.TechnicalTest.Domain.CQRS.Components.ProductComponent.Queries;
using EA.Tekton.TechnicalTest.Domain.Dto;
using EA.Tekton.TechnicalTest.Domain.Interfaces;

using Mapster;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace EA.Tekton.TechnicalTest.WebApi.Controllers.v1;
    
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductsController(IMediator mediator, ILogger<ProductsController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ILogger<ProductsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [ApiVersion("1")]
    [HttpGet("{productId:int}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResponseService<ProductResponse>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseService<ProductResponse>), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetProductsByProductIdAsync(int productId)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var result = await _mediator.Send(new GetProductByProductIdQuery(productId));

        var isResult = result.Value != null!;

        var response = new ResponseService<ProductResponse>
        {
            HttpStatusCode = isResult ? HttpStatusCode.OK : HttpStatusCode.NoContent,
            Status = isResult,
            Message = isResult ? CrossEnum.Status.Ok.ToStringAttribute() : CrossEnum.Status.Error.ToStringAttribute(),
            Data = result.Value
        };


        stopwatch.Stop();

        _logger.LogInformation($"{nameof(GetProductsByProductIdAsync)} Tiempo de Ejecución: {stopwatch.ElapsedMilliseconds} ms");

        return Ok(response);
    }
    
    [ApiVersion("1")]
    [HttpGet("{page:int}/{limit:int}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResponseService<QueryMultipleResponse<ProductDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseService<QueryMultipleResponse<ProductDto>>), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetProductsAllAsync(int? page, int? limit)
    {
        var result = await _mediator.Send(new GetProductAllQuery(page ?? 1, limit ?? 1000, "ProductId"));
        
        var response = new ResponseService<QueryMultipleResponse<ProductDto>>
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
    [Produces(MediaTypeNames.Application.Json, Type = typeof(ProductRequest))]
    public async Task<IActionResult> PostProductsAsync([FromBody] ProductRequest request)
    {
        var objRequest = request.Adapt<ProductDto>();

        objRequest.CreationUser = HttpContext.User.Identity?.Name!;
        objRequest.CreationDate = DateTime.Now;
        objRequest.Deleted = false;

        var status = await _mediator.Send(new CreateProductCommand(objRequest));

        return Ok(new ResponseService<int>
        {
            HttpStatusCode = status.IsSuccess ? HttpStatusCode.Created : HttpStatusCode.InternalServerError,
            Status = status.IsSuccess,
            Data = status.Value.productId
        });
    }

    [ApiVersion("1")]
    [HttpPut("{productId:int}")]
    [DisableRequestSizeLimit]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResponseService<bool>), (int)HttpStatusCode.OK)]
    [Produces(MediaTypeNames.Application.Json, Type = typeof(ProductRequest))]
    public async Task<IActionResult> PutProductsAsync(int productId, [FromBody] ProductRequest request)
    {
        if (productId != request.ProductId)
            return BadRequest();

        var objRequest = request.Adapt<ProductDto>();
            
        objRequest.ModificationUser = HttpContext.User.Identity?.Name!;
        objRequest.ModificationDate = DateTime.Now;

        var status = await _mediator.Send(new UpdateProductCommand(productId, objRequest));
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
    [HttpDelete("{productId:int}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ResponseService<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteProductsAsync(int productId)
    {
        var status = await _mediator.Send(new DeleteProductCommand(productId));
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
    [HttpGet("states/{status:bool}")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(ResponseService<IEnumerable<ProductDto>>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ResponseService<IEnumerable<ProductDto>>), (int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> GetAllStateByStatusIdAsync(int statusId)
    {
        var result = await _mediator.Send(new GetAllStateForeignKeyByStatusIdQuery(statusId));
        var resultDto = result.Value as ProductDto[] ?? result.Value.ToArray();
        var response = new ResponseService<IEnumerable<ProductDto>>
        {
            HttpStatusCode = result.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.NoContent,
            Status = result.IsSuccess,
            Message = result.IsSuccess ? CrossEnum.Status.Ok.ToStringAttribute() : CrossEnum.Status.Error.ToStringAttribute(),
            Data = resultDto
        };
        return Ok(response);
    }
        
    
}