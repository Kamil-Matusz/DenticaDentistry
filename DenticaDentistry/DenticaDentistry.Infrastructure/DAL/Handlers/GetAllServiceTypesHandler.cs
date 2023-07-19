﻿using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers;

internal class GetAllServiceTypesHandler : IQueryHandler<GetAllServiceTypes, IEnumerable<ServiceTypeDto>>
{
    private readonly DenticaDentistryDbContext _dbContext;

    public GetAllServiceTypesHandler(DenticaDentistryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<ServiceTypeDto>> HandlerAsync(GetAllServiceTypes query)
    {
        var serviceTypes = await _dbContext.ServiceTypes
            .AsNoTracking()
            .ToListAsync();

        return serviceTypes.Select(s => s.AsServiceTypesDto());
    }
}