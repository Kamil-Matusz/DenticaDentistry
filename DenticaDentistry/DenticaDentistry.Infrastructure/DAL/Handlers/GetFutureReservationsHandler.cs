using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Core.Models;
using DenticaDentistry.Core.ValueObjects;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal sealed class GetFutureReservationsHandler : IQueryHandler<GetFutureDentistReservations, IEnumerable<DentistReservationsDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetFutureReservationsHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DentistReservationsDto>> HandlerAsync(GetFutureDentistReservations query)
    {
        var pager = new Pager(query.PageIndex, query.PageSize);
        var userId = new UserId(query.UserId);
        var now = DateTime.Now;
        
        var dentistI = await _dbContext.Dentists
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .Select(x => new 
            {
                DentistId = x.DentistId.Value
            })
            .SingleOrDefaultAsync();
        
        var reservations = await _dbContext.Reservations
            .AsNoTracking()
            .Where(x => x.ReservationDate > now && x.DentistId.Equals(dentistI))
            .Paginate(pager)
            .ToListAsync();
        
        return reservations.Select(x => x.AsDentistReservationsDto());

    }
}