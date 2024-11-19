using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Flight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository flightRepository;

        public FlightService(IFlightRepository flightRepository) {
            this.flightRepository = flightRepository;
        }
        public async Task<IEnumerable<FlightGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data = await flightRepository.GetAll()
                    .Include(flight => flight.Airplane)
                    .Include(flight => flight.DepartureAirport)
                    .Include(flight => flight.DestinationAirport)
                    .Include(flight => flight.FlightSchedules).Select(flight => new FlightGetAllDto
                    {
                        FlightId = flight.FlightId,
                        AirplaneName = flight.Airplane != null ? flight.Airplane.AirplaneName : "Unknown",
                        DepartureAirportName = flight.DepartureAirport != null ? flight.DepartureAirport.AirportName : "Unknown",
                        DestinationAirportName = flight.DestinationAirport != null ? flight.DestinationAirport.AirportName : "Unknown",
                        EconomyPrice = flight.EconomyPrice,
                        BusinessPrice = flight.BusinessPrice,
                        FlightSchedules = flight.FlightSchedules.Select(schedule => new FlightScheduleDto
                        {
                            DepartureTime = schedule.DepartureTime,
                            ArrivalTime = schedule.ArrivalTime
                        }).ToList()
                    }).ToListAsync(cancellationToken);
            return data;

        }
    }
}
