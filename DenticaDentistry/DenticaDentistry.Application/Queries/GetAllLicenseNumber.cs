using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetAllLicenseNumber : IQuery<IEnumerable<DentistWithLicenseNumberDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}