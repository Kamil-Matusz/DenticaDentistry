using System.Collections;
using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Core.ValueObjects;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal sealed class GetFutureUserReservationsHandler : IQueryHandler<GetFutureUserReservations, IEnumerable<ReservationDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetFutureUserReservationsHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ReservationDto>> HandlerAsync(GetFutureUserReservations query)
    {
        var userId = new UserId(query.UserId);
        var now = DateTime.Now;
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Where(x => x.UserId == userId && x.ReservationDate > now)
            .ToListAsync();
        
        return reservations.Select(x => x.AsReservationDto());
    }
}