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
}