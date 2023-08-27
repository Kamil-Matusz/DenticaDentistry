using DenticaDentistry.Application.DTO;
using DenticaDentistry.Core.Entities;

namespace DenticaDentistry.Infrastructure.DAL.Handlers;

internal static class Extensions
{
    public static ReservationDto AsReservationDto(this Reservation entity) => new()
    {
        ReservationId = entity.ReservationId,
        BookerName = entity.BookerName,
        DentistIndustryId = entity.DentistIndustryId,
        ReservationDate = entity.ReservationDate
    };

    public static DentistIndustryDto AsDentistIndustryDto(this DentistIndustry entity) => new()
    {
        DentistIndustryId = entity.DentistIndustryId,
        ServiceTypeId = entity.ServiceTypeId,
        Name = entity.Name,
        Price = entity.Price,
        Description = entity.Description
    };
    
    public static UserDto AsUsersDto(this User entity) => new()
    {
        UserId = entity.UserId,
        Username = entity.Username,
        Fullname = entity.Fullname
    };

    public static ServiceTypeDto AsServiceTypesDto(this ServiceType entity) => new()
    {
        Name = entity.ServiceTypeName
    };

    public static DentistWithLicenseNumberDto AsDentistWithLicenseNumberDto(this Dentist entity) => new()
    {
        DentistId = entity.DentistId,
        LicenseNumber = entity.LicenseNumber
    };
}