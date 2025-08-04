using MediatR;
using Booking.Application.Common.Model;
using Booking.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Queries.GetAllPaged
{
    public class GetAllApartmentsPagedHandler : IRequestHandler<GetAllApartmentsPagedQuery, PagedResult<ApartmentDto>>
    {
        private readonly IApartmentRepository _repository;
        public GetAllApartmentsPagedHandler(IApartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<ApartmentDto>> Handle(GetAllApartmentsPagedQuery request, CancellationToken cancellationToken)
        {
            if (request.PageIndex < 0)
            {
                request.PageIndex = 0;
            }

            if (request.PageSize <= 0)
            {
                request.PageSize = 10;
            }

            if(request.PageSize > 10)
            {
                request.PageSize = 10; 
            }

            var (apartments, totalCount) = await _repository.GetPagedAsync(
                request.PageIndex,
                request.PageSize,
                request.SortBy,
                request.SortDescending,
                request.SearchTerm,
                cancellationToken);
            var apartmentDto = apartments.Select(apartment => new ApartmentDto
            {
                OwnerId = apartment.OwnerId,
                Name = apartment.Name,
                Address = apartment.Address,
                Price = apartment.Price,
                Description = apartment.Decription,
                CleaningFee = apartment.CleaningFee,
                Amenities = apartment.Amenities.ToList()
            }).ToList();

            return new PagedResult<ApartmentDto>(apartmentDto, request.PageIndex, request.PageSize, totalCount);
        }
    }
}
