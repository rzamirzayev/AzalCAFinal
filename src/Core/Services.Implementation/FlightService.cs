﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Flight;
using Services.FlightSchedule;
using Services.Passanger;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                DepartureAirport = flight.DepartureAirport?.AirportName,
                DestinationAirport = flight.DestinationAirport?.AirportName,
                FlightDate = flight.FlightTime.ToString(),
                EconomyPrice=flight.EconomyPrice,
                BusinessPrice=flight.BusinessPrice,
                Airplane=flight.Airplane?.AirplaneName,
                FlightSchedules = flight.FlightSchedules.Select(sch => new FlightScheduleDto
                {
                    FlightId = sch.FlightId,
                    DepartureTime = sch.DepartureTime,
                    ArrivalTime = sch.ArrivalTime
                }).ToList()

            }).ToList();

            return flightDtos;
        }
        public async Task<RoundTripFlightSearchResultDto> GetAvailableRoundTripFlights(string departureCity,string destinationCity,string flightDate,string returnFlightDate,int adultCount,int childCount,int infantCount)
        {
            var outboundFlights = await GetAvailableFlights(departureCity, destinationCity, flightDate, adultCount, childCount, infantCount);
            var returnFlights = await GetAvailableFlights(destinationCity, departureCity, returnFlightDate, adultCount, childCount, infantCount);

            return new RoundTripFlightSearchResultDto
            {
                OutboundFlights = outboundFlights,
                ReturnFlights = returnFlights
            };
        }

        public async Task<EditFlightDto> EditAsync(EditFlightDto model, CancellationToken cancellationToken = default)
        {
            var entity = await flightRepository.GetAsync(m => m.FlightId == model.FlightId, cancellationToken);
            var time=await flightScheduleRepository.GetAsync(fs=>fs.FlightId==model.FlightId, cancellationToken);
            entity.AirplaneId = model.AirplaneId;
            entity.DepartureAirportId = model.DepartureAirportId;
            entity.DestinationAirportId = model.DestinationAirportId;
            entity.EconomyPrice=model.EconomyPrice;
            entity.BusinessPrice=model.BusinessPrice;
            entity.FlightTime= model.FlightTime;
            time.ArrivalTime= model.ArrivalTime;
            time.DepartureTime=model.DepartureTime;
            flightRepository.Edit(entity);
            flightScheduleRepository.Edit(time);
            await flightRepository.SaveAsync();
            await flightScheduleRepository.SaveAsync();

            return model;
        }
        public async Task<List<FlightSearchDto>> GetFlightByCity(string city,string flightDate,string type)
        {
            var data = await flightRepository.GetAll()
                .Include(flight => flight.Airplane)
                .Include(flight => flight.DepartureAirport).ThenInclude(a => a.City)
                .Include(flight => flight.DestinationAirport).ThenInclude(b => b.City)
                .Include(flight => flight.FlightSchedules)
                .ToListAsync();

            var filteredFlights = type.ToLower() == "from"
               ? data.Where(f => f.DepartureAirport?.City?.CityName == city &&
                                 f.FlightTime.ToString() == flightDate).ToList()
               : data.Where(f => f.DestinationAirport?.City?.CityName == city &&
                                 f.FlightTime.ToString() == flightDate).ToList();


            var flightDtos = filteredFlights.Select(flight => new FlightSearchDto
            {

                Id = flight.FlightId,
                DepartureAirport = flight.DepartureAirport?.AirportName,
                DestinationAirport = flight.DestinationAirport?.AirportName,
                FlightDate = flight.FlightTime.ToString(),
                EconomyPrice = flight.EconomyPrice,
                BusinessPrice = flight.BusinessPrice,
                Airplane = flight.Airplane?.AirplaneName,

                FlightSchedules = flight.FlightSchedules.Select(sch => new FlightScheduleDto
                {
                    FlightId = sch.FlightId,
                    DepartureTime = sch.DepartureTime,
                    ArrivalTime = sch.ArrivalTime,
                    onTheAir = GetFlightStatus(flightDate, sch.DepartureTime, sch.ArrivalTime)
                }).ToList(),


            }).ToList();

            return flightDtos;
        }
        string GetFlightStatus(string flightDate, TimeSpan departureTime, TimeSpan arrivalTime)
        {
            DateTime now = DateTime.Now;
            DateTime onlyDate = now.Date;
            TimeSpan onlyOclock = now.TimeOfDay; 

            if (!DateTime.TryParse(flightDate, out DateTime flightDateTime))
                return "Invalid Flight Date";

            if (departureTime == TimeSpan.Zero || arrivalTime == TimeSpan.Zero)
                return "Schedule Not Available";

            if (onlyDate > flightDateTime.Date)
            {
                return "Landed"; 
            }

            if (onlyDate < flightDateTime.Date)
            {
                return "Not Departed Yet"; 
            }

            if (onlyOclock < departureTime)
            {
                return "Not Departed Yet"; 
            }
            else if (onlyOclock >= departureTime && onlyOclock <= arrivalTime)
            {
                return "On the Air"; 
            }
            else
            {
                return "Landed"; 
            }
        }


    }
}
