using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Core.ValueObjects;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal sealed class GetAllUserReservationsHandler : IQueryHandler<GetAllUserReservations, IEnumerable<ReservationDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetAllUserReservationsHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ReservationDto>> HandlerAsync(GetAllUserReservations query)
    {
        var userId = new UserId(query.UserId);
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .ToListAsync();
        
        return reservations.Select(x => x.AsReservationDto());
    }
}