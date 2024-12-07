namespace Services.Airport
{
    public interface IAirportService
    {
        Task<IEnumerable<AirportGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AirportGetAllDto> GetById(int id, CancellationToken cancellationToken = default);
        Task<int?> GetIdByNameAsync(string airportName, CancellationToken cancellationToken = default);




    }
}
