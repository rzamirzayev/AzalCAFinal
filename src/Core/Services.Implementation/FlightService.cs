using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Flight;
using Services.FlightSchedule;
using Services.Passanger;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Implementation
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository flightRepository;
        private readonly IAirplanesRepository airplaneRepoitory;
        private readonly IFlightScheduleRepository flightScheduleRepository;
        private readonly IAirportRepoitory airportRepository;

        public FlightService(IFlightRepository flightRepository,IAirplanesRepository airplaneRepoitory,IFlightScheduleRepository flightScheduleRepository,IAirportRepoitory airportRepository)
        {
            this.flightRepository = flightRepository;
            this.airplaneRepoitory = airplaneRepoitory;
            this.flightScheduleRepository = flightScheduleRepository;
            this.airportRepository = airportRepository;

        }
        public async Task<IEnumerable<FlightGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var data = await flightRepository.GetAll()
                    .Include(flight => flight.Airplane)
                    .Include(flight => flight.DepartureAirport)
                    .Include(flight => flight.DestinationAirport)
                    .Include(flight => flight.FlightSchedules)
                    .Include(flight => flight.TicketBookings).ThenInclude(ticket => ticket.Passenger)
                    .Select(flight => new FlightGetAllDto
                    {
                        FlightId = flight.FlightId,
                        AirplaneName = flight.Airplane != null ? flight.Airplane.AirplaneName : "Unknown",
                        DepartureAirportName = flight.DepartureAirport != null ? flight.DepartureAirport.AirportName : "Unknown",
                        DestinationAirportName = flight.DestinationAirport != null ? flight.DestinationAirport.AirportName : "Unknown",
                        EconomyPrice = flight.EconomyPrice,
                        BusinessPrice = flight.BusinessPrice,
                        FlightDate=flight.FlightTime,
                        FlightSchedules = flight.FlightSchedules.Select(schedule => new FlightScheduleDto
                        {
                            DepartureTime = schedule.DepartureTime,
                            ArrivalTime = schedule.ArrivalTime
                        }).ToList(),
                        Passangers = flight.TicketBookings
                    .Select(ticket => new PassangerGetAllDto
                    {
                        PassangerId = ticket.Passenger.PassangerId,
                        Name = ticket.Passenger.Name,
                        Surname = ticket.Passenger.Surname,
                        Phone = ticket.Passenger.Phone,
                        Email = ticket.Passenger.Email,
                        FinCode = ticket.Passenger.FinCode,
                        DateOfBirth = ticket.Passenger.DateOfBirth,
                        Gender = ticket.Passenger.Gender

                    }).ToList()
                    }).ToListAsync(cancellationToken);

            return data;

        }

   
        public async Task<AddFlightResponseDto> AddAsync(AddFlightRequestDto model, CancellationToken cancellationToken = default)
        {
            var flight = new Domain.Entities.Flight
            {
                AirplaneId = model.AirplaneId,
                DepartureAirportId = model.DepartureAirportId,
                DestinationAirportId = model.DestinationAirportId,
                EconomyPrice = model.EconomyPrice,
                BusinessPrice = model.BusinessPrice,
                FlightTime=model.FlightTime,
            };

            await flightRepository.AddAsync(flight, cancellationToken);
            await flightRepository.SaveAsync();


            var flightSchedule = new Domain.Entities.FlightSchedule
            {
                FlightId = flight.FlightId, 
                DepartureTime = model.DepartureTime,
                ArrivalTime = model.ArrivalTime
            };

            await flightScheduleRepository.AddAsync(flightSchedule, cancellationToken);
            await flightScheduleRepository.SaveAsync(cancellationToken);

            return new AddFlightResponseDto
            {
                FlightId = flight.FlightId,
                AirplaneName = (await airplaneRepoitory.GetAsync(a=>a.AirplaneId==model.AirplaneId, cancellationToken)).AirplaneName,
                DepartureAirport = (await airportRepository.GetAsync(a=>a.AirportId==model.DepartureAirportId, cancellationToken)).AirportName,
                DestinationAirport = (await airportRepository.GetAsync(a=>a.AirportId==model.DestinationAirportId, cancellationToken)).AirportName,
                EconomyPrice = flight.EconomyPrice,
                BusinessPrice = flight.BusinessPrice,
                DepartureTime = flightSchedule.DepartureTime.ToString(),
                ArrivalTime = flightSchedule.ArrivalTime.ToString(),
                FlightTime=flight.FlightTime.ToString(),
               
            };
        }

        public async Task<AddFlightResponseDto> GetById(int id, CancellationToken cancellationToken = default)
        {
            var flight = await flightRepository.GetAsync(f=>f.FlightId==id,cancellationToken);
            var time=await flightScheduleRepository.GetAsync(f=>f.FlightId== id,cancellationToken);

            return new AddFlightResponseDto
            {
                FlightId = flight.FlightId,
                AirplaneName = (await airplaneRepoitory.GetAsync(a => a.AirplaneId == flight.AirplaneId, cancellationToken)).AirplaneName,
                DepartureAirport = (await airportRepository.GetAsync(a => a.AirportId == flight.DepartureAirportId, cancellationToken)).AirportName,
                DestinationAirport = (await airportRepository.GetAsync(a => a.AirportId == flight.DestinationAirportId, cancellationToken)).AirportName,
                EconomyPrice = flight.EconomyPrice,
                BusinessPrice = flight.BusinessPrice,
                DepartureTime = time.DepartureTime.ToString(),
                ArrivalTime = time.ArrivalTime.ToString(),
                FlightTime = flight.FlightTime.ToString(),

            };
        }

        public async Task<List<FlightSearchDto>> GetAvailableFlights(string departureCity, string destinationCity, string flightDate, int adultCount, int childCount, int infantCount)
        {
            
            var data = await flightRepository.GetAll()
        .Include(flight => flight.Airplane)
        .Include(flight => flight.DepartureAirport).ThenInclude(a=>a.City)
        .Include(flight => flight.DestinationAirport).ThenInclude(b=>b.City)
        .Include(flight => flight.FlightSchedules)
        .Include(flight => flight.TicketBookings).ThenInclude(ticket => ticket.Passenger)
        .ToListAsync();
            var a= data.Where(f => f.DepartureAirport?.City?.CityName == departureCity &&
                   f.DestinationAirport?.City?.CityName == destinationCity &&
                    f.FlightTime.ToString() == flightDate).ToList();

            var flightDtos = a.Select(flight => new FlightSearchDto
            {
                Id =flight.FlightId,
                DepartureAirport = flight.DepartureAirport.AirportName,
                DestinationAirport = flight.DestinationAirport.AirportName,
                FlightDate = flight.FlightTime.ToString(),
                EconomyPrice=flight.EconomyPrice,
                BusinessPrice=flight.BusinessPrice,
                Airplane=flight.Airplane.AirplaneName,
                FlightSchedules = flight.FlightSchedules.Select(sch => new FlightScheduleDto
                {
                    FlightId = sch.FlightId,
                    DepartureTime = sch.DepartureTime,
                    ArrivalTime = sch.ArrivalTime
                }).ToList()

            }).ToList();

            return flightDtos;
        }

        //    public async Task<List<AddFlightResponseDto>> GetAvailableFlights(string departureCity, string destinationCity, DateOnly flightDate, int adultCount, int childCount, int infantCount)
        //    {
        //        var flights = await flightRepository.GetAll()
        //.Include(f => f.DepartureAirport)
        //.Include(f => f.DestinationAirport)
        //.Where(f => f.DepartureAirport.City.CityName == departureCity &&
        //            f.DestinationAirport.City.CityName == destinationCity &&
        //            f.FlightTime == flightDate)
        //.ToListAsync();


        //var filteredFlights = flights.Where(f => f.TicketBookings
        //        .Count(ticket => !ticket.Passenger.IsChild) >= adultCount &&
        //        f.TicketBookings.Count(ticket => ticket.Passenger.IsChild) >= childCount &&
        //        f.TicketBookings.Count(ticket => ticket.Passenger.IsInfant) >= infantCount)
        //    .ToList();

        //    }
    }
}
