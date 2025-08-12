using AutoMapper;
using Booking.Application.Common.DTOs.BookingDTOs;
using Booking.Application.Features.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Bookings.Queries.GetAll
{
    public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, List<BookingDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public GetAllBookingsHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<List<BookingDto>> Handle(GetAllBookingsQuery query, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if(userId == null)
            {
                throw new UnauthorizedAccessException("User not authenticated!");
            }

            var bookingsPerUser = await _bookingRepository.GetAllBookingsPerUser(userId);
            return _mapper.Map<List<BookingDto>>(bookingsPerUser);  
        }
    }
}
