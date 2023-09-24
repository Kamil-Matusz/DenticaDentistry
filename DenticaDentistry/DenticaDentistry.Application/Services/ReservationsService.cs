using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Services;

public class ReservationsService : IReservationsService
{
    private readonly IClock _clock;
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserRepository _userRepository;

    public ReservationsService(IClock clock,IReservationRepository reservationRepository, IUserRepository userRepository)
    {
        
        _clock = clock;
        _reservationRepository = reservationRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
    {
        var reservation = await _reservationRepository.GetAllReservationAsync();
        
            return reservation
                .SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
            {
                ReservationId = x.ReservationId,
                DentistIndustryId = x.DentistIndustryId,
                BookerName = x.BookerName,
                ReservationDate = x.ReservationDate
            });
    }

    public async Task<ReservationDto> GetReservationAsync(Guid id)
    {
        var reservation = await GetAllReservationsAsync();
        return reservation.SingleOrDefault(x => x.ReservationId == id);
    }

    public async Task<Guid?> CreateReservationAsync(CreateReservation command)
    {
        var dentistIndustryId = command.DentistIndustryId;
        var dentistIndustryName = await _reservationRepository.GetReservationAsync(dentistIndustryId);
        if (dentistIndustryName is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId, command.DentistIndustryId,command.BookerName, command.ReservationDate,command.UserId,command.DentistId);
        
        dentistIndustryName.AddReservation(reservation);
        await _reservationRepository.UpdateAsync(dentistIndustryName);
        return reservation.ReservationId;
    }
    
    public async Task<bool> UpdateReservationDateAsync(ChangeReservationDate command)
    {
        var dentistIndustry = await GetReservationsAsync(command.ReservationId);
        if (dentistIndustry is null)
        {
            return false;
        }

        var reservationId = command.ReservationId;
        var existingReservation = dentistIndustry.Reservations.SingleOrDefault(x => x.ReservationId == reservationId);
        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.ReservationDate <= DateTime.UtcNow)
        {
            return false;
        }
        existingReservation.ChangeReservationDate(command.ReservationDate);
        await _reservationRepository.UpdateAsync(dentistIndustry);
        return true;
    }
    
    public async Task<bool> DeleteReservationAsync(DeleteReservation command)
    {
        var dentistIndustryId = await GetReservationsAsync(command.ReservationId);
        if (dentistIndustryId is null)
        {
            return false;
        }

        var existingReservation = dentistIndustryId.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

       dentistIndustryId.RemoveReservation(command.ReservationId);
       await _reservationRepository.DeleteAsync(dentistIndustryId);
       return true;
    }

    public async Task<string> GetUserEmail(Guid userId)
    {
        var userEmail = await _userRepository.GetUserEmail(userId);
        return userEmail;
    }

    private async Task<DentistIndustry> GetReservationsAsync(Guid reservationId)
    {
        var reservations = await _reservationRepository.GetAllReservationAsync();
            
        return reservations.SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));
    }

}