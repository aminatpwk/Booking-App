using AutoMapper;
using Booking.Application.Common.DTOs;
using Booking.Application.Common.Services;
using Booking.Application.Features.Apartments;
using Booking.Application.Features.Emails;
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
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly INotificationService _notificationService;
        public CreateBookingHandler(IBookingRepository bookingRepository, IMapper mapper, ICurrentUserService currentUserService, IApartmentRepository apartmentRepository, IUserRepository userRepository, IEmailTemplateService emailTemplateService, IEmailService emailService, IConfiguration configuration, IHttpContextAccessor contextAccessor, INotificationService notificationService)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _apartmentRepository = apartmentRepository;
            _userRepository = userRepository;
            _emailTemplateService = emailTemplateService;
            _emailService = emailService;
            _configuration = configuration;
            _contextAccessor = contextAccessor;
            _notificationService = notificationService;
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

            var notificationDto = new NotificationDto
            {
                BookingId = bookingEntity.Id,
                ApartmentId = apartment.Id,
                CheckIn = bookingDto.Start,
                CheckOut = bookingDto.End,
                GuestId = userId,
                CreatedAt = DateTime.UtcNow,
                Status = BookingStatus.PendingApproval
            };

            await _notificationService.SendNotificationToOwnerForBookingCreation(apartment.OwnerId, notificationDto);
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
