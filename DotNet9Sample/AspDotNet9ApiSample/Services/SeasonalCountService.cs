using AspDotNet9ApiSample.Data;
using AspDotNet9ApiSample.Data.Entities.CarEntities;
using AspDotNet9ApiSample.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Services
{
    public class SeasonalCountService
    {
        private readonly CarClinchDbContext _context;
        public SeasonalCountService(CarClinchDbContext context) =>
            _context = context;

        public async Task<IEnumerable<SeasonalCountDto>> GetSeasonalCountsAsync(
            DateTime from,
            DateTime to,
            string? make = null,
            string? model = null,
            string? bodyType = null)
        {
            // base query: filter by date
            var q = _context.Set<ArchivedCar>()
                .Where(c => c.CreatedDate >= from && c.CreatedDate <= to);

            // optional filters
            if (!string.IsNullOrWhiteSpace(make))
                q = q.Where(c => c.Make == make);
            if (!string.IsNullOrWhiteSpace(model))
                q = q.Where(c => c.Model == model);
            if (!string.IsNullOrWhiteSpace(bodyType))
                q = q.Where(c => c.BodyType == bodyType);

            // group by month
            return await q
                .GroupBy(c => c.CreatedDate.Month)
                .Select(g => new SeasonalCountDto {
                    Month = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToListAsync();
        }
    }
}
