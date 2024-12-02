
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Passanger
{
    public interface IPassangerService
    {
        Task<AddPassangerResponseDto> AddAsync(AddPassangerRequestDto model, CancellationToken cancellationToken = default);
    }
}
