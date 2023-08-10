using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Dentica_Dentistry.Api.Controllers;

[Route("dentists")]
public class DentistController : ControllerBase
{
    private readonly ICommandHandler<AddLicenseNumber> _addDentistLicenseHandler;
    private readonly IQueryHandler<GetAllLicenseNumber, IEnumerable<DentistWithLicenseNumberDto>> _getAllLicenseNumber;
    private readonly IQueryHandler<GetAllDentists, IEnumerable<DentistDto>> _getAllDentists;

    public DentistController(ICommandHandler<AddLicenseNumber> addDentistLicenseHandler, IQueryHandler<GetAllLicenseNumber, IEnumerable<DentistWithLicenseNumberDto>> getAllLicenseNumber, IQueryHandler<GetAllDentists, IEnumerable<DentistDto>> getAllDentists)
    {
        _addDentistLicenseHandler = addDentistLicenseHandler;
        _getAllLicenseNumber = getAllLicenseNumber;
        _getAllDentists = getAllDentists;
    }
    
    [Authorize(Roles = "admin")]
    [HttpPatch("{dentistId:guid}/addLicensePlate")]
    [SwaggerOperation("Added license number to the dentist")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddLicensePlate(Guid dentistId, AddLicenseNumber command)
    {
        await _addDentistLicenseHandler.HandlerAsync(command with { DentistId = dentistId });
        return Ok();
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    [Route("allLicenseNumberList")]
    [SwaggerOperation("Get list of all dentists license")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DentistWithLicenseNumberDto>>> GetAllLicenseList([FromQuery] GetAllLicenseNumber query) 
        => Ok(await _getAllLicenseNumber.HandlerAsync(query));
    
    [HttpGet]
    [Route("getAllDentists")]
    [SwaggerOperation("Get list of all dentists")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DentistWithLicenseNumberDto>>> GetAllDentists([FromQuery] GetAllDentists query) 
        => Ok(await _getAllDentists.HandlerAsync(query));
}