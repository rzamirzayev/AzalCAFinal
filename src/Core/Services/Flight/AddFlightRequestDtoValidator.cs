using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Flight
{
    public class AddFlightRequestDtoValidator:AbstractValidator<AddFlightRequestDto>
    {
        public AddFlightRequestDtoValidator()
        {
            RuleFor(f => f.AirplaneId)
        .NotNull()
        .WithMessage("Airplane seçilmelidir.");

            RuleFor(f => f.DepartureAirportId)
                .NotNull()
                .WithMessage("Qalxis havalimanı seçilmelidir.");

            RuleFor(f => f.DestinationAirportId)
                .NotNull()
                .WithMessage("Enis havalimanı seçilmelidir.")
                .NotEqual(f => f.DepartureAirportId)
                .WithMessage("Qalxis ve Enis havalimanı eyni olmamalidir.");

            RuleFor(f => f.FlightTime)
                .NotNull()
                .WithMessage("Uçuş Tarixinin bildirmelisiz.");
                //.GreaterThan(DateTime.UtcNow)
                //.WithMessage("Uçuş tarixi kecmis bir tarix olmamaldir.");
            RuleFor(f => f.EconomyPrice)
                .NotNull()
                .WithMessage("Economy Price bos olmamalidir");
            RuleFor(f => f.BusinessPrice)
                 .NotNull()
                .WithMessage("Economy Price bos olmamalidir");
            RuleFor(f => f.DepartureTime)
                .NotNull()
                .WithMessage("Qalxis vaxti bos olmamalidir");

            RuleFor(f => f.ArrivalTime)
                .NotNull()
                .WithMessage("Enis vaxti bos olmamalidir");


        }
    }
}
