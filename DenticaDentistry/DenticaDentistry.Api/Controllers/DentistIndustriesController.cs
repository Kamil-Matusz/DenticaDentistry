using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Application.Services;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IQueryHandler<GetDentistService,DentistIndustryDto> _getDentistServiceHandler;

    public DentistIndustriesController(ICommandHandler<CreateDentistService> createDentistServiceHandler, ICommandHandler<DeleteDentistService> deleteDentistServiceHandler, ICommandHandler<ChangeDentistServiceName> changeDentistServiceNameHandler, ICommandHandler<ChangeDentistServicePrice> changeDentistServicePriceHandler, IQueryHandler<GetAllDentistServices, IEnumerable<DentistIndustryDto>> getAllDentistServicesHandler, IQueryHandler<GetDentistService, DentistIndustryDto> getDentistServiceHandler)
    {
        _createDentistServiceHandler = createDentistServiceHandler;
        _deleteDentistServiceHandler = deleteDentistServiceHandler;
        _changeDentistServiceNameHandler = changeDentistServiceNameHandler;
        _changeDentistServicePriceHandler = changeDentistServicePriceHandler;
        _getAllDentistServicesHandler = getAllDentistServicesHandler;
        _getDentistServiceHandler = getDentistServiceHandler;
    }

    [HttpGet]
    [SwaggerOperation("Displaying all dentist services")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DentistIndustryDto>>> GetAllServices([FromQuery] GetAllDentistServices query) 
        => Ok(await _getAllDentistServicesHandler.HandlerAsync(query));
    
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

    [Authorize(Roles = "admin")]
    [HttpPost]
    [SwaggerOperation("Creating new dentist service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddDentistService(CreateDentistService command)
    {
        await _createDentistServiceHandler.HandlerAsync(command);
        return Ok();
    }

    [Authorize(Roles = "admin")]
    [HttpPut("ChangeServiceName/{dentistIndustryId:int}")]
    [SwaggerOperation("Change dentist service name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ChangeServiceName(int dentistIndustryId,ChangeDentistServiceName command)
    {
        await _changeDentistServiceNameHandler.HandlerAsync(command with { DentistIndustryId = dentistIndustryId });
        return Ok();
    }

    [Authorize(Roles = "admin")]
    [HttpPut("ChangeServicePrice/{dentistIndustryId:int}")]
    [SwaggerOperation("Change dentist service price")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> ChangeServicePrice(int dentistIndustryId, ChangeDentistServicePrice command)
    {
        await _changeDentistServicePriceHandler.HandlerAsync(command with { DentistIndustryId = dentistIndustryId });
        return Ok();
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{dentistIndustryId:int}")]
    [SwaggerOperation("Delete dentist service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteService(int dentistIndustryId)
    {
        await _deleteDentistServiceHandler.HandlerAsync(new DeleteDentistService(dentistIndustryId));
        return Ok();
    }

    [HttpGet("{dentistIndustryId:int}")]
    [SwaggerOperation("Displaying Dentist Service By Id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DentistIndustryDto>> GetDentistServiceById(int dentistIndustryId, GetDentistService query) =>
        Ok(await _getDentistServiceHandler.HandlerAsync(query));
}