using Booking.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booking.Application.Features.Owners.Commands;
using Booking.Application.Features.Owners.Queries.CheckOwnerProfile;
using Booking.Application.Features.Owners.Queries.GetOwnerProfile;

namespace Booking_App.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OwnerController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> CreateOwner([FromBody] OwnerDto ownerDto)
        {
            var command = new CreateOwnerCommand { OwnerDto = ownerDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);  
        }

        [HttpGet("check-owner-profile")]
        public async Task<ActionResult<CheckOwnerProfileResult>> CheckOwnerProfile()
        {
            var query = new CheckOwnerProfileQuery();
            var result = await _sender.Send(query);
            return Ok(new CheckOwnerProfileResult
            {
                HasOwnerProfile = result.HasOwnerProfile
            });
        }

        [HttpGet("profile")]
        public async Task<ActionResult<CheckOwnerProfileResult>> GetOwnerProfile()
        {
            var query = new GetOwnerProfileQuery();
            var result = await _sender.Send(query);
            if (result == null)
            {
                return NotFound(new { message = "Owner profile not found!" });
            }

            return Ok(new OwnerDto
            {
                UserId = result.UserId,
                IdentityCardNumber = result.IdentityCardNumber,
                BankAccount = result.BankAccount,
                PhoneNumber = result.PhoneNumber
            });
        }
    }
}
