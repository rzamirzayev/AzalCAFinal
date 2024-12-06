namespace Services.Airport
{
    public interface IAirportService
    {
        Task<IEnumerable<AirportGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AirportGetAllDto> GetById(int id, CancellationToken cancellationToken = default);
        Task<AirportGetAllDto> GetByName(string name, CancellationToken cancellationToken = default);



    }
}
