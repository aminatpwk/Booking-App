using Booking.Domain.Apartments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Queries.GetAll
{
    public class GetAllApartmentsHandler : IRequestHandler<GetAllApartmentsQuery, List<ApartmentDto>>
    {
        private readonly IApartmentRepository _repository;
        public GetAllApartmentsHandler(IApartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ApartmentDto>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
        {
            var apartments = await _repository.GetAll();
            if(apartments is null || !apartments.Any())
            {
                return new List<ApartmentDto>();
            }

            var result = apartments.Select(apartment => new ApartmentDto
            {
                OwnerId = apartment.OwnerId,
                Name = apartment.Name,
                Address = apartment.Address,
                Price = apartment.Price,
                Description = apartment.Decription,
                CleaningFee = apartment.CleaningFee,
                Amenities = apartment.Amenities.ToList()
            }).ToList();

            return result;
        }
    }
}
