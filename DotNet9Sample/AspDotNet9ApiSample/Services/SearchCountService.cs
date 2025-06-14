using AspDotNet9ApiSample.Data.Entities.CarEntities;
using AspDotNet9ApiSample.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Services
{
    public class SearchCountService
    {
        private readonly CarClinchDbContext _context;

        public SearchCountService(CarClinchDbContext context) =>
            _context = context;

        public async Task<IEnumerable<SearchCountDto>> GetSearchCountsAsync(DateTime from, DateTime to)
        {
            return await _context.Set<ArchivedCar>()
                .Where(c => c.CreatedDate >= from && c.CreatedDate <= to)
                .GroupBy(c => new {
                    c.Make,
                    c.Model,
                    c.BodyType,
                    Date = c.CreatedDate.Date
                })
                .Select(g => new SearchCountDto {
                    Make     = g.Key.Make,
                    Model    = g.Key.Model,
                    BodyType = g.Key.BodyType,
                    Date     = g.Key.Date,
                    Count    = g.Count()
                })
                .ToListAsync();
        }
    }
}
