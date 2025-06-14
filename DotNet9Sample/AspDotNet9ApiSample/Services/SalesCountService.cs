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
    public class SalesCountService
    {
        private readonly CarClinchDbContext _context;

        public SalesCountService(CarClinchDbContext context) =>
            _context = context;

        public async Task<IEnumerable<SalesCountDto>> GetSalesCountsAsync(DateTime from, DateTime to)
        {
            return await _context.Set<ArchivedCar>()
                .Where(c => c.ArchivedDate >= from && c.ArchivedDate <= to)
                .GroupBy(c => new {
                    c.Make,
                    c.Model,
                    c.BodyType,
                    Date = c.ArchivedDate.Date
                })
                .Select(g => new SalesCountDto {
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
