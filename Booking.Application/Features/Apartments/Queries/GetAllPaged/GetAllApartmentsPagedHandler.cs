using AutoMapper;
using Booking.Application.Common.DTOs;
using Booking.Application.Common.Model;
using Booking.Domain.Apartments;
using MediatR;
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
        private readonly IMapper _mapper;
        public GetAllApartmentsPagedHandler(IApartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
                request.SearchTerm,
                request.SortBy,
                request.SortDescending, 
                request.Country,
                request.City,
                request.Type,
                request.MinPrice,
                request.MaxPrice,
                cancellationToken);

            var apartmentDto = _mapper.Map<List<ApartmentDto>>(apartments);

            return new PagedResult<ApartmentDto>(apartmentDto, request.PageIndex, request.PageSize, totalCount);
        }
    }
}
