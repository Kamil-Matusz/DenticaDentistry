using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Application.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult> AddDentistService(CreateDentistService command)
    {
        await _createDentistServiceHandler.HandlerAsync(command);
        return NoContent();
    }

    [HttpPut("{derntistIndustryId:int}")]
    public async Task<ActionResult> ChangeServiceName(int dentistIndustryId,ChangeDentistServiceName command)
    {
        await _changeDentistServiceNameHandler.HandlerAsync(command with { DentistIndustryId = dentistIndustryId });
        return NoContent();
    }
    
    [HttpDelete("{dentistIndustryId:int}")]
    public async Task<ActionResult> DeleteService(int dentistIndustryId)
    {
        await _deleteDentistServiceHandler.HandlerAsync(new DeleteDentistService(dentistIndustryId));
        return NoContent();
    }
}