using AspDotNet9ApiSample.Data;
using AspDotNet9ApiSample.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Services
{
    public class SearchCountService
    {
        private readonly CarClinchDbContext _context;
        public SearchCountService(CarClinchDbContext context) =>
            _context = context;

        // #2: All SUVs over time
        public async Task<IEnumerable<SearchCountDto>> GetSuvCountsAsync()
        {
            var sql = @"
                SELECT
                  YEAR(CreatedDate) AS [Year],
                  MONTH(CreatedDate) AS [Month],
                  COUNT(*)         AS [Count]
                FROM ArchivedCar
                WHERE BodyType = 'SUV'
                GROUP BY YEAR(CreatedDate), MONTH(CreatedDate)
                ORDER BY [Year], [Month]";

            return await _context
                .Set<SearchCountDto>()
                .FromSqlRaw(sql)
                .ToListAsync();
        }

        // #3: 2024-only SUVs
        public async Task<IEnumerable<SearchCountDto>> GetAnnualSuvCountsAsync()
        {
            var sql = @"
                SELECT
                  YEAR(CreatedDate) AS [Year],
                  MONTH(CreatedDate) AS [Month],
                  COUNT(*)         AS [Count]
                FROM ArchivedCar
                WHERE BodyType = 'SUV'
                  AND CreatedDate BETWEEN '2024-01-01' AND '2024-12-31'
                GROUP BY YEAR(CreatedDate), MONTH(CreatedDate)
                ORDER BY [Year], [Month]";

            return await _context
                .Set<SearchCountDto>()
                .FromSqlRaw(sql)
                .ToListAsync();
        }
    }
}
