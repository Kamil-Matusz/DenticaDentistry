using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Infrastructure.DAL;
using DenticaDentistry.Infrastructure.DAL.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DenticaDentistry.Core.Models;

namespace Dentica_Dentistry.Infrastructure.DAL.Handlers
{
    internal class GetAllDentistServicesHandler : IQueryHandler<GetAllDentistServices, IEnumerable<DentistIndustryDto>>
    {
        private readonly DenticaDentistryDbContext _dbContext;

        public GetAllDentistServicesHandler(DenticaDentistryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DentistIndustryDto>> HandlerAsync(GetAllDentistServices query)
        {
            var pager = new Pager(query.PageIndex, query.PageSize);
            
            var servicesList = _dbContext.DentistIndustries.AsNoTracking();
            var paginatedServicesList = servicesList.Paginate(pager);
            var services = await paginatedServicesList.ToListAsync();

            return services.Select(x => x.AsDentistIndustryDto());
        }
    }
}