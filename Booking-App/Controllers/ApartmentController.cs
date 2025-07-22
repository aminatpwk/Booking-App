using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Domain.Apartments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApartmentController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> Create([FromBody] ApartmentDto apartmentDto)
        {
            var command = new CreateApartmentCommand { ApartmentDto = apartmentDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }
    }
}
