using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Airplane
{
    public interface IAirplaneService
    {
        Task<IEnumerable<AirplaneGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AirplaneGetAllDto> GetById(int id, CancellationToken cancellationToken = default);
        Task<int?> GetIdByNameAsync(string airplaneName, CancellationToken cancellationToken = default);


    }
}
