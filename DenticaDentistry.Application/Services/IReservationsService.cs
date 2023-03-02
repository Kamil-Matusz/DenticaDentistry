using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;
using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Application.Services;

public interface IReservationsService
{
    IEnumerable<ReservationDto> GetAllReservations();
    ReservationDto GetReservation(Guid id);
    Guid? CreateReservation(CreateReservation command);
    bool UpdateReservationDate(ChangeReservationDate command);
    bool DeleteReservation(DeleteReservation command);
}