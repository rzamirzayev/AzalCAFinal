using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AddServiceResponseDto> AddAsync(AddServiceRequestDto model, CancellationToken cancellationToken = default);
        Task<EditServiceDto> EditAsync(EditServiceDto model,CancellationToken cancellationToken=default);
        Task<ServiceGetAllDto> GetById(int id, CancellationToken cancellationToken = default);

        Task RemoveAsync(int id, CancellationToken cancellationToken=default);
    }
}
