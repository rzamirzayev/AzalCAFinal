using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Airplane;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    class AirplaneService : IAirplaneService
    {
        private readonly IAirplanesRepository airplanesRepository;

        public AirplaneService(IAirplanesRepository airplanesRepository) { this.airplanesRepository = airplanesRepository; }
        public async Task<IEnumerable<AirplaneGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data = await airplanesRepository.GetAll().Select(m => new AirplaneGetAllDto
            {
                AirplaneId = m.AirplaneId,
                AirplaneName = m.AirplaneName,

            }).ToListAsync(cancellationToken);
            return data;
        }

        public async Task<AirplaneGetAllDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            var airplane = await airplanesRepository.GetAsync(b => b.AirplaneId == id, cancellationToken);

            return new AirplaneGetAllDto
            {
                AirplaneId=airplane.AirplaneId,
                AirplaneName=airplane.AirplaneName,
                
            };
        }
        public async Task<int?> GetIdByNameAsync(string airplaneName, CancellationToken cancellationToken = default)
        {
            var airplane = await airplanesRepository.GetAsync(a => a.AirplaneName == airplaneName, cancellationToken);
            return airplane?.AirplaneId;
        }
    }
}
