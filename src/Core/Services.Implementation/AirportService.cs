using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Airplane;
using Services.Airport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    internal class AirportService : IAirportService
    {
        private readonly IAirportRepoitory airportRepoitory;

        public AirportService(IAirportRepoitory airportRepoitory) { this.airportRepoitory = airportRepoitory; }
        public async Task<IEnumerable<AirportGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data=await airportRepoitory.GetAll().Select(m=>new AirportGetAllDto
            {
                AirportId=m.AirportId,
                AirportName= m.AirportName,
            }).ToListAsync(cancellationToken);

            return data;
        }

        public async Task<AirportGetAllDto> GetById(int id, CancellationToken cancellationToken = default)
        {
                var airport = await airportRepoitory.GetAsync(b => b.AirportId == id, cancellationToken);

                return new AirportGetAllDto
                {
                    AirportName = airport.AirportName,
                    AirportId=airport.AirportId,
                };
            
        }

    }
}
