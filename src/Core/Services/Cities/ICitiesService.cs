
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Cities
{
    public interface ICitiesService
    {
        Task<IEnumerable<CitiesGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);

    }
}
