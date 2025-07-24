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
            //apartmentdto = dmth qe objektin qe kemi ne body do e vendosim aty
            var command = new CreateApartmentCommand { ApartmentDto = apartmentDto };

            //duhet ti bejme handle result qe te ktheje ok ose status code tjeter
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }
    }
}
