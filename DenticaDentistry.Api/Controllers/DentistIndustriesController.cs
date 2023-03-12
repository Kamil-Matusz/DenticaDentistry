using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;
using Dentica_Dentistry.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dentica_Dentistry.Api.Controllers;

[Route("services")]
public class DentistIndustriesController : ControllerBase
{
    private readonly IDentistsService _dentistsService;

    public DentistIndustriesController(IDentistsService dentistsService)
    {
        _dentistsService = dentistsService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DentistIndustryDto>>> GetAllServices() => Ok(await _dentistsService.GetAllServicesAsync());
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DentistIndustryDto>> GetService(int id)
    {
        var service = await _dentistsService.GetServiceAsync(id);
        if (service is null)
        {
            return NotFound();
        }

        return Ok(service);
    }

    [HttpPost]
    public async Task<ActionResult> AddDentistService(CreateDentistService command)
    {
        var dentistIndustryId = await _dentistsService.CreateDentistServiceAsync(command with { DentistIndustryId = new int() });
        if (dentistIndustryId is null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetService), new { dentistIndustryId }, null);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> ChangeServiceName(ChangeDentistServiceName command)
    {
        if (await _dentistsService.UpdateDentistServiceName(command with { DentistIndustryId = command.DentistIndustryId}))
        {
            return NoContent();
        }

        return NotFound();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteService(int id)
    {
        if (await _dentistsService.DeleteServiceAsync(new DeleteService(id)))
        {
            return NoContent();
        }

        return NotFound();
    }
}