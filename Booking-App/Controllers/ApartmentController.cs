using Booking.Application.Features.Apartments.Commands.CreateApartment;
using Booking.Application.Features.Apartments.Commands.DeleteApartment;
using Booking.Application.Features.Apartments.Commands.UpdateApartment;
using Booking.Application.Features.Apartments.Queries.Get;
using Booking.Application.Features.Apartments.Queries.GetAll;
using Booking.Application.Features.Apartments.Queries.GetAllPaged;
using Booking.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Booking.Application.Common.Services;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ApartmentController(ISender _sender, ICacheService _cacheService) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "Owner")]
        public async Task<IResult> Create([FromBody] ApartmentDto apartmentDto)
        {
            var command = new CreateApartmentCommand { ApartmentDto = apartmentDto };

            //TO DO: duhet ti bejme handle result qe te ktheje ok ose status code tjeter
            var result = await _sender.Send(command);
            _cacheService.Remove("apartments_list");
            return Results.Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(Guid id)
        {
            var query = new GetApartmentQuery { Id = id };
            var result = await _sender.Send(query);
            return Results.Ok(result);
        }

        [HttpGet]
        public async Task<IResult> GetAllApartments()
        {
            var query = new GetAllApartmentsQuery();
            var result = await _sender.Send(query);
            return Results.Ok(result);
        }

        [HttpGet("paged")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IResult> GetAllApartmentsPaged([FromQuery] GetAllApartmentsPagedQuery query)
        { 
            var result = await _sender.Send(query);
            return Results.Ok(result);
        }

        [HttpDelete("{id}"), Authorize(Roles = "Owner")]
        public async Task<IResult> Delete(Guid id)
        {
            var command = new DeleteApartmentCommand { Id = id };
            var result = await _sender.Send(command);
            _cacheService.Remove($"apartments_{id}");
            _cacheService.Remove("apartments_list");
            return Results.Ok(result);
        }

        [HttpPut("{id}"), Authorize(Roles = "Owner")]
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
