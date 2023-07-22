using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetDentistService : IQuery<DentistIndustryDto>
{
    public int DentistServiceId { get; set; }
}