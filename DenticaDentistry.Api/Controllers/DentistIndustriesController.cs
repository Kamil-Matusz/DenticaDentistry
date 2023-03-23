using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DenticaDentistry.Api.Controllers;

[Route("services")]
public class DentistIndustriesController : ControllerBase
{
    private readonly ICommandHandler<CreateDentistService> _createDentistServiceHandler;
    private readonly ICommandHandler<DeleteDentistService> _deleteDentistServiceHandler;
    private readonly ICommandHandler<ChangeDentistServiceName> _changeDentistServiceNameHandler;
    private readonly ICommandHandler<ChangeDentistServicePrice> _changeDentistServicePriceHandler;

    private readonly IQueryHandler<GetAllDentistServices, IEnumerable<DentistIndustryDto>> _getAllDentistServicesHandler;

    public DentistIndustriesController(ICommandHandler<CreateDentistService> createDentistServiceHandler, ICommandHandler<DeleteDentistService> deleteDentistServiceHandler, ICommandHandler<ChangeDentistServiceName> changeDentistServiceNameHandler, ICommandHandler<ChangeDentistServicePrice> changeDentistServicePriceHandler, IQueryHandler<GetAllDentistServices, IEnumerable<DentistIndustryDto>> getAllDentistServicesHandler)
    {
        _createDentistServiceHandler = createDentistServiceHandler;
        _deleteDentistServiceHandler = deleteDentistServiceHandler;
        _changeDentistServiceNameHandler = changeDentistServiceNameHandler;
        _changeDentistServicePriceHandler = changeDentistServicePriceHandler;
        _getAllDentistServicesHandler = getAllDentistServicesHandler;
    }

    [HttpGet]
    [SwaggerOperation("Displaying all dentist services")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DentistIndustryDto>>> GetAllServices([FromQuery] GetAllDentistServices query) => Ok(await _getAllDentistServicesHandler.HandlerAsync(query));
    
    /*[HttpGet("{id:int}")]
    public async Task<ActionResult<DentistIndustryDto>> GetService(int id)
    {
        var service = await _dentistsService.GetServiceAsync(id);
        if (service is null)
        {
            return NotFound();
        }

        return Ok(service);
    }*/

    [HttpPost]
    [SwaggerOperation("Creating new dentist service")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddDentistService(CreateDentistService command)
    {
        await _createDentistServiceHandler.HandlerAsync(command);
        return NoContent();
    }

    [HttpPut("ChangeServiceName/{dentistIndustryId:int}")]
    [SwaggerOperation("Change dentist service name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ChangeServiceName(int dentistIndustryId,ChangeDentistServiceName command)
    {
        await _changeDentistServiceNameHandler.HandlerAsync(command with { DentistIndustryId = dentistIndustryId });
        return Ok();
    }

    [HttpPut("ChangeServicePrice/{dentistIndustryId:int}")]
    [SwaggerOperation("Change dentist service price")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ChangeServicePrice(int dentistIndustryId, ChangeDentistServicePrice command)
    {
        await _changeDentistServicePriceHandler.HandlerAsync(command with { DentistIndustryId = dentistIndustryId });
        return Ok();
    }
    
    [HttpDelete("{dentistIndustryId:int}")]
    [SwaggerOperation("Delete dentist service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteService(int dentistIndustryId)
    {
        await _deleteDentistServiceHandler.HandlerAsync(new DeleteDentistService(dentistIndustryId));
        return Ok();
    }
}