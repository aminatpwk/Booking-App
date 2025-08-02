using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Application.Features.Apartments.Commands.DeleteApartment;
using Booking.Application.Features.Apartments.Commands.UpdateApartment;
using Booking.Application.Features.Apartments.Queries.Get;
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

            //TO DO: duhet ti bejme handle result qe te ktheje ok ose status code tjeter
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(Guid id)
        {
            var query = new GetApartmentQuery { Id = id };
            var result = await _sender.Send(query);
            return Results.Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(Guid id)
        {
            var command = new DeleteApartmentCommand { Id = id };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update(Guid id, [FromBody] ApartmentDetailDto apartmentDetailDto)
        {
            var command = new UpdateApartmentCommand { 
                Id = id,
                ApartmentDetailDto = apartmentDetailDto 
            };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }
    }
}
