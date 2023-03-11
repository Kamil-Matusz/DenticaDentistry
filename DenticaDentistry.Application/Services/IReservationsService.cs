using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;
using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Application.Services;

public interface IReservationsService
{
    Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
    Task<ReservationDto> GetReservationAsync(Guid id);
    Task<Guid?> CreateReservationAsync(CreateReservation command);
    Task<bool> UpdateReservationDateAsync(ChangeReservationDate command);
    Task<bool> DeleteReservationAsync(DeleteReservation command);
}