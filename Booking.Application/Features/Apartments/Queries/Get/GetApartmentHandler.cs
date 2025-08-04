using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Queries.Get
{
    public class GetApartmentHandler : IRequestHandler<GetApartmentQuery, ApartmentDetailDto>
    {
        public readonly IApartmentRepository _apartmentRepository;
        public GetApartmentHandler(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<ApartmentDetailDto> Handle(GetApartmentQuery request, CancellationToken cancellationToken)
        {
            var apartment = await _apartmentRepository.GetById(request.Id);
            if (apartment is null)
            {
                throw new Exception("Apartment not found!");
            }

            var apartmentDetailDto = new ApartmentDetailDto
            {
                Name = apartment.Name,
                Country = apartment.Country,
                City = apartment.City,
                Description = apartment.Decription,
                Address = apartment.Address,
                Price = apartment.Price,
                CleaningFee = apartment.CleaningFee,
                Bedrooms = apartment.Bedrooms,
                Bathrooms = apartment.Bathrooms,
                MaxGuests = apartment.MaxGuests,
                Type = apartment.Type,
                Amenities = apartment.Amenities?.ToList(),
                IsActive = apartment.IsActive,
                IsAvailable = apartment.IsAvailable,
                OwnerId = apartment.OwnerId
            };

            return apartmentDetailDto;
        }
    } 
}
