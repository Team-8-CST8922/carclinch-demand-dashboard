using AspDotNet9ApiSample.Data;
using AspDotNet9ApiSample.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Services
{
    public class SalesCountService
    {
        private readonly CarClinchDbContext _context;
        public SalesCountService(CarClinchDbContext context) =>
            _context = context;

        public async Task<IEnumerable<SalesCountDto>> GetSalesCountsAsync()
        {
            var sql = @"
                SELECT
                  YEAR(CreatedDate) AS [Year],
                  MONTH(CreatedDate) AS [Month],
                  COUNT(*)         AS [Count]
                FROM ArchivedCar
                GROUP BY YEAR(CreatedDate), MONTH(CreatedDate)
                ORDER BY [Year], [Month]";

            return await _context
                .Set<SalesCountDto>()
                .FromSqlRaw(sql)
                .ToListAsync();
        }
    }
}
