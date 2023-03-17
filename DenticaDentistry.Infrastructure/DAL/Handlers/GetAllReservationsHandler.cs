using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal sealed class GetAllReservationsHandler : IQueryHandler<GetAllReservations, IEnumerable<ReservationDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetAllReservationsHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<ReservationDto>> HandlerAsync(GetAllReservations query)
    {
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .ToListAsync();

        return reservations.Select(x => x.AsReservationDto());
    }
}