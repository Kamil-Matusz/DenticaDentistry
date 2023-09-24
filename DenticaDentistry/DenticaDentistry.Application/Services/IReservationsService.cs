using DenticaDentistry.Core.Entities;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Services;

public interface IReservationsService
{
    Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
    Task<ReservationDto> GetReservationAsync(Guid id);
    Task<Guid?> CreateReservationAsync(CreateReservation command);
    Task<bool> UpdateReservationDateAsync(ChangeReservationDate command);
    Task<bool> DeleteReservationAsync(DeleteReservation command);
    Task<string> GetUserEmail(Guid userId);
}