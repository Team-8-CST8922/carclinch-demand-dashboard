using AspDotNet9ApiSample.Data;
using AspDotNet9ApiSample.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNet9ApiSample.Services
{
    public class DurationService
    {
        private readonly CarClinchDbContext _context;
        public DurationService(CarClinchDbContext context) =>
            _context = context;

        public async Task<AverageDaysDto> GetAverageDaysInSystemAsync()
        {
            var sql = @"
                SELECT AVG(CAST(DATEDIFF(DAY, CreatedDate, ArchivedDate) AS FLOAT)) 
                       AS AverageDaysInSystem
                FROM ArchivedCar
                WHERE ArchivedDate IS NOT NULL";

            var result = await _context
                .Set<AverageDaysDto>()
                .FromSqlRaw(sql)
                .ToListAsync();

            return result.FirstOrDefault();
        }
    }
}
