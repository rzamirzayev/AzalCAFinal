using Repositories;
using Services.FlightSchedule;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Services.Airplane;

namespace Services.Implementation
{
    internal class FlightScheduleService : IFlightScheduleService
    {
        private readonly IFlightScheduleRepository flightScheduleRepository;

        public FlightScheduleService(IFlightScheduleRepository flightScheduleRepository) { this.flightScheduleRepository = flightScheduleRepository; }
        public async Task<IEnumerable<FlightScheduleGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data = await flightScheduleRepository.GetAll().Select(fs => new FlightScheduleGetAllDto
            {
                DepartureTime = fs.DepartureTime,
                ArrivalTime = fs.ArrivalTime,
            }).ToListAsync(cancellationToken);
            return data;
        }

        public async Task<FlightScheduleGetAllDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            var flightSchedule = await flightScheduleRepository.GetAsync(b => b.FlightId == id, cancellationToken);

            return new FlightScheduleGetAllDto
            {
                ArrivalTime = flightSchedule.ArrivalTime,
                DepartureTime = flightSchedule.DepartureTime,

            };
        }
    }
}
