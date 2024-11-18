using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Cities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
     class CitiesService:ICitiesService
    {
        private readonly ICityRepository cityRepository;

        public CitiesService(ICityRepository cityRepository) {
            this.cityRepository = cityRepository;
        }
        public async Task<IEnumerable<CitiesGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data = await cityRepository.GetAll()
    .Include(city => city.Country)  
    .Select(m => new CitiesGetAllDto
    {
        CityName = m.CityName,
        CountryName = m.Country != null ? m.Country.CountryName : string.Empty 
    })
    .ToListAsync(cancellationToken);
            return data;
        }

    }
}
