using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Dentica_Dentistry.Api.Controllers;
[Route("serviceTypes")]
public class ServiceTypesController : ControllerBase
{
    private readonly ICommandHandler<CreateServiceType> _createServiceTypeHandler;
    private readonly IQueryHandler<GetAllServiceTypes, IEnumerable<ServiceTypeDto>> _getAllServiceTypeHandler;

    public ServiceTypesController(ICommandHandler<CreateServiceType> createServiceTypeHandler, IQueryHandler<GetAllServiceTypes, IEnumerable<ServiceTypeDto>> getAllServiceTypeHandler)
    {
        _createServiceTypeHandler = createServiceTypeHandler;
        _getAllServiceTypeHandler = getAllServiceTypeHandler;
    }

    [HttpGet]
    [SwaggerOperation("Displaying all service types")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ServiceTypeDto>>> GetAllServiceTypes([FromQuery] GetAllServiceTypes query) => Ok(await _getAllServiceTypeHandler.HandlerAsync(query));

    [HttpPost]
    [SwaggerOperation("Creating new service type")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddServiceType(CreateServiceType command)
    {
        await _createServiceTypeHandler.HandlerAsync(command);
        return Ok();
    }
}