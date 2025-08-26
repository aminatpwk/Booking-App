using AutoMapper;
using Booking.Application.Common.DTOs;
using Booking.Application.Common.Events.Bookings;
using Booking.Application.Common.Services.Notifications;
using Booking.Application.Features.Apartments;
using Booking.Application.Features.Emails;
using Booking.Application.Features.Owners;
using Booking.Application.Features.Users;
using Booking.Domain.Bookings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Booking.Application.Features.Bookings.Commands
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Guid>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMediator _mediator;
        //private readonly IOwnerRepository _ownerRepository;
        public CreateBookingHandler(IBookingRepository bookingRepository, ICurrentUserService currentUserService, IApartmentRepository apartmentRepository, IEmailTemplateService emailTemplateService, IEmailService emailService, IHttpContextAccessor contextAccessor, IMediator mediator)
        {
            _bookingRepository = bookingRepository;
            _currentUserService = currentUserService;
            _apartmentRepository = apartmentRepository;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
            _contextAccessor = contextAccessor;
            _mediator = mediator;
            //_ownerRepository = ownerRepository;
        }

        public async Task<Guid> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            var bookingDto = command.CreateBookingDto;
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

            //TO DO: me implementu logjiken e llogaritjes se cmimit dhe totalit sipas kritereve
            decimal priceForPeriod = 35;
            decimal cleaningFee = 5;
            decimal amenitiesUpCharge = 5;
            var bookingEntity = BookingEntity.Create(bookingDto.ApartmentId, userId, bookingDto.Start, bookingDto.End, priceForPeriod, cleaningFee, amenitiesUpCharge);

            bookingEntity.GenerateConfirmationToken();
            await _bookingRepository.Add(bookingEntity);

            await SendConfirmationEmail(bookingEntity);

            //var ownerModel = await _ownerRepository.GetById(apartment.OwnerId)

            var bookingCreationEvent = new BookingCreatedEvent(bookingEntity.Id, apartment.Id, apartment.OwnerId, apartment.Name, bookingDto.Start, bookingDto.End);

            await _mediator.Publish(bookingCreationEvent, cancellationToken);
            return bookingEntity.Id;
        }

        private async Task SendConfirmationEmail(BookingEntity booking)
        {
            try
            {
                var userEmail = _currentUserService.Email;
               
                if(userEmail == null)
                {
                    throw new Exception("User not found for booking confirmation email!");
                }

                var request = _contextAccessor.HttpContext?.Request;
                var baseUrl = $"{request.Scheme}://{request?.Host}";
                var confirmationUrl = $"{baseUrl}/api/v1/Booking/confirm/{booking.ConfirmationToken}";
                var cancellationUrl = $"{baseUrl}/api/v1/Booking/cancel/{booking.ConfirmationToken}";

                var templateData = new Dictionary<string, object>
                {
                    {"ConfirmationUrl", confirmationUrl },
                    {"CancellationUrl", cancellationUrl },
                    {"StartDate", booking.Start.ToString("yyyy-MM-dd") },
                    {"EndDate", booking.End.ToString("yyyy-MM-dd") },
                    {"TotalPrice", booking.TotalPrice.ToString() }
                };

                var email = await _emailTemplateService.CreateEmailFromTemplateAsync(userEmail, "BookingConfirmation", templateData);

                await _emailService.SendEmailAsync(email);

            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}
