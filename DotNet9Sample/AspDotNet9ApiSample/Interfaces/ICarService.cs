namespace AspDotNet9ApiSample.Interfaces
{
    public interface ICarService
    {
        Task<int> GetCarCountByDateAsync(DateTime date, CancellationToken cancellationToken);
    }
}
