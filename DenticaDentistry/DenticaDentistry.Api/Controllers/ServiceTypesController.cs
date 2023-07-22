using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Dentica_Dentistry.Api.Controllers;
[Route("serviceTypes")]
public class ServiceTypesController : ControllerBase
{
    private readonly ICommandHandler<CreateServiceType> _createServiceTypeHandler;
    private readonly ICommandHandler<ChangeServiceTypeName> _changeServiceTypeNameHandler;
    private readonly IQueryHandler<GetAllServiceTypes, IEnumerable<ServiceTypeDto>> _getAllServiceTypeHandler;

    public ServiceTypesController(ICommandHandler<CreateServiceType> createServiceTypeHandler, IQueryHandler<GetAllServiceTypes, IEnumerable<ServiceTypeDto>> getAllServiceTypeHandler, ICommandHandler<ChangeServiceTypeName> changeServiceTypeNameHandler)
    {
        _createServiceTypeHandler = createServiceTypeHandler;
        _getAllServiceTypeHandler = getAllServiceTypeHandler;
        _changeServiceTypeNameHandler = changeServiceTypeNameHandler;
    }

    [HttpGet]
    [SwaggerOperation("Displaying all service types")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ServiceTypeDto>>> GetAllServiceTypes([FromQuery] GetAllServiceTypes query) => Ok(await _getAllServiceTypeHandler.HandlerAsync(query));
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    [SwaggerOperation("Creating new service type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddServiceType(CreateServiceType command)
    {
        await _createServiceTypeHandler.HandlerAsync(command);
        return Ok();
    }

    [Authorize(Roles = "admin")]
    [HttpPut("{serviceTypeId:int}")]
    [SwaggerOperation("Change dentist service name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ChangeServiceTypeName(int serviceTypeId, ChangeServiceTypeName command)
    {
        await _changeServiceTypeNameHandler.HandlerAsync(command with {ServiceTypeId = serviceTypeId});
        return Ok();
    }
}