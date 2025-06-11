using AspDotNet9ApiSample.Data;
using AspDotNet9ApiSample.Data.Entities.CarEntities;
using AspDotNet9ApiSample.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspDotNet9ApiSample.Services
{
    public class CarService : ICarService
    {
        private readonly CarClinchDbContext _carClinchDbContext;

        public CarService(CarClinchDbContext applicationDbContext)
        {
            _carClinchDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        public async Task<int> GetCarCountByDateAsync(DateTime date, CancellationToken cancellationToken)
        {
            IQueryable<Car> carEntity = _carClinchDbContext.Car.AsNoTracking().AsQueryable();

            return await carEntity.CountAsync(o => o.CreatedDate <= date, cancellationToken);
        }
    }
}
