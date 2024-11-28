using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repositories;
using Services.Airplane;
using Services.Airport;
using Services.Flight;
using System.Threading;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class FlightController : Controller
    {
        private readonly IFlightService flightService;
        private readonly IAirportService airportService;
        private readonly IAirplaneService airplaneService;

        public FlightController(IFlightService flightService,IAirportService airportService,IAirplaneService airplaneService) 
        {
            this.airportService = airportService;
            this.airplaneService = airplaneService;
            this.flightService = flightService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await flightService.GetAllAsync();
            return View(data);
        }
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
        public async Task<IActionResult> Create(FlightViewModel model)
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
                FlightTime=model.FlightDate
            };

            await flightService.AddAsync(addFlightRequest);


            return RedirectToAction("Index");
        }

    //    public async Task<IActionResult> Edit(int id)
    //    {
    //        var flight = await flightService.GetById(id);
    //        var airplanes = await airplaneService.GetAllAsync();
    //        var airports = await airportService.GetAllAsync();
    //        var viewModel = new FlightViewModel
    //        {
    //            Airplanes = airplanes.ToList(),
    //            Airports = airports.ToList(),
    //            SelectedAirplaneId = (int)flight.AirplaneId,
    //     SelectedDepartureAirportId= (int)flight.DepartureAirportId,
    //    SelectedDestinationAirportId= (int)flight.DestinationAirportId,
    //    EconomyPrice =flight.EconomyPrice,
    // BusinessPrice =flight.BusinessPrice,
    //    FlightDate=flight.FlightDate
    //};
    //        return View(viewModel);
    //    }



    }
}
