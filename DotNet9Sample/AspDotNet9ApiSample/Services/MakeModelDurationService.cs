using AspDotNet9ApiSample.Data;
using AspDotNet9ApiSample.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Services
{
    public class MakeModelDurationService
    {
        private readonly CarClinchDbContext _context;
        public MakeModelDurationService(CarClinchDbContext context) =>
            _context = context;

        public async Task<IEnumerable<MakeModelDurationDto>> GetByMakeModelAsync()
        {
            var sql = @"
                SELECT
                  Make,
                  Model,
                  AVG(CAST(DATEDIFF(DAY, CreatedDate, ArchivedDate) AS FLOAT))
                    AS AverageDaysInSystem
                FROM ArchivedCar
                WHERE ArchivedDate IS NOT NULL
                GROUP BY Make, Model
                ORDER BY Make, Model";

            return await _context
                .Set<MakeModelDurationDto>()
                .FromSqlRaw(sql)
                .ToListAsync();
        }
    }
}
