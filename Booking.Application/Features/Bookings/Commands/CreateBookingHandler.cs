using AutoMapper;
using Booking.Application.Features.Apartments;
using Booking.Application.Features.Users;
using Booking.Domain.Bookings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Bookings.Commands
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApartmentRepository _apartmentRepository;
        public CreateBookingHandler(IBookingRepository bookingRepository, IMapper mapper, ICurrentUserService currentUserService, IApartmentRepository apartmentRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Guid> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var bookingDto = command.BookingDto;
            var userId = _currentUserService.UserId;
            var apartment = await _apartmentRepository.GetById(bookingDto.ApartmentId);
            if(apartment is null)
            {
                throw new Exception("This apartment doesn't exist!");
            }

            bool isAvailable = await _bookingRepository.IsApartmentAvailable(bookingDto.ApartmentId, bookingDto.Start, bookingDto.End);
            if (!isAvailable)
            {
                throw new Exception("This apartment is already booked for this period!");
            }

            decimal priceForPeriod = 35;
            decimal cleaningFee = 5;
            decimal amenitiesUpCharge = 5;
            var bookingEntity = BookingEntity.Create(bookingDto.ApartmentId, userId, bookingDto.Start, bookingDto.End, priceForPeriod, cleaningFee, amenitiesUpCharge);
            await _bookingRepository.Add(bookingEntity);
            return bookingEntity.Id;
        }
    }
}
