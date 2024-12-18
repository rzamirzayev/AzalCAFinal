﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;
using Services.Airplane;
using Services.Airport;
using Services.Flight;
using Services.FlightSchedule;
using System.Threading;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class FlightController : Controller
    {
        private readonly IFlightService flightService;
        private readonly IFlightScheduleService flightScheduleService;
        private readonly IAirportService airportService;
        private readonly IAirplaneService airplaneService;

        public FlightController(IFlightService flightService,IAirportService airportService,IAirplaneService airplaneService,IFlightScheduleService flightScheduleService) 
        {
            this.airportService = airportService;
            this.airplaneService = airplaneService;
            this.flightService = flightService;
            this.flightScheduleService = flightScheduleService;
        }
        [Authorize(Policy ="admin.flight.get")]
        public async Task<IActionResult> Index()
        {
            var data = await flightService.GetAllAsync();
            return View(data);
        }
        [Authorize(Policy = "admin.flight.create")]

        public async Task<IActionResult> Create()
        {
            var airplanes = await airplaneService.GetAllAsync();
            var airports = await airportService.GetAllAsync();
            var viewModel = new FlightViewModel
            {
                Airplanes = airplanes.ToList(),
                Airports = airports.ToList(),
            };
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Policy = "admin.flight.create")]

        public async Task<IActionResult> Create([FromForm]FlightViewModel model)
        {

            var addFlightRequest = new AddFlightRequestDto
            {
                AirplaneId = model.SelectedAirplaneId,
                DepartureAirportId = model.SelectedDepartureAirportId,
                DestinationAirportId = model.SelectedDestinationAirportId,
                EconomyPrice = model.EconomyPrice,
                BusinessPrice = model.BusinessPrice,
                DepartureTime = model.DepartureTime,
                ArrivalTime = model.ArrivalTime,
                FlightTime = model.FlightDate
            };

            await flightService.AddAsync(addFlightRequest);


            return RedirectToAction("Index");
        }
        [Authorize(Policy ="admin.flight.edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await flightService.GetById(id);

            var airplanes = await airplaneService.GetAllAsync();
            var airports = await airportService.GetAllAsync();
            var times = await flightScheduleService.GetById(response.FlightId);
            var airplaneId = await airplaneService.GetIdByNameAsync(response.AirplaneName);
            var departureAirportId = await airportService.GetIdByNameAsync(response.DepartureAirport);
            var destinationAirportId = await airportService.GetIdByNameAsync(response.DestinationAirport);

            if (airplaneId == null || departureAirportId == null || destinationAirportId == null)
            {
                return NotFound("Airplane or Airport not found");
            }

            var viewModel = new FlightViewModel
            {
                FlightId=response.FlightId,
                Airplanes = airplanes.ToList(),
                Airports = airports.ToList(),
                SelectedAirplaneId = airplaneId.Value,
                SelectedDepartureAirportId = departureAirportId.Value,
                SelectedDestinationAirportId = destinationAirportId.Value,
                EconomyPrice = response.EconomyPrice,
                BusinessPrice = response.BusinessPrice,
                FlightDate= DateOnly.Parse(response.FlightTime),
                DepartureTime=times.DepartureTime,
                ArrivalTime= times.ArrivalTime,

            };

            return View(viewModel);
        }
        [Authorize(Policy = "admin.flight.edit")]

        [HttpPost]
        public async Task<IActionResult> Edit(EditFlightDto model)
        {

            await flightService.EditAsync(model);
            return RedirectToAction("Index");
        }




    }
}
