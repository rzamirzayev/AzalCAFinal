﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Flight
{
    public interface IFlightService
    {
        Task<IEnumerable<FlightGetAllDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AddFlightResponseDto> GetById(int id,CancellationToken cancellationToken = default);
        Task<AddFlightResponseDto> AddAsync(AddFlightRequestDto model, CancellationToken cancellationToken = default);
        Task<EditFlightDto> EditAsync(EditFlightDto model,CancellationToken cancellationToken = default);
        //Task<List<AddFlightResponseDto>> GetAvailableFlights(string departureCity, string destinationCity, DateOnly flightDate, int adultCount, int childCount, int infantCount);
        Task<List<FlightSearchDto>> GetAvailableFlights(string departureCity, string destinationCity, string flightDate, int adultCount, int childCount, int infantCount);
        Task<List<FlightSearchDto>> GetFlightByCity(string city, string flightDate, string type);
        Task<RoundTripFlightSearchResultDto> GetAvailableRoundTripFlights(string departureCity,string destinationCity,string flightDate,string returnFlightDate,int adultCount,int childCount,int infantCount);


    }
}
