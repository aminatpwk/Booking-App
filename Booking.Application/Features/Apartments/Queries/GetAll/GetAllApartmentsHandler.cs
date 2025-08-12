using AutoMapper;
using MediatR;
using Booking.Application.Common.DTOs;
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
        private readonly IMapper _mapper;
        public GetAllApartmentsHandler(IApartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ApartmentDto>> Handle(GetAllApartmentsQuery request, CancellationToken cancellationToken)
        {
            var apartments = await _repository.GetAll();
            if(apartments is null || !apartments.Any())
            {
                return new List<ApartmentDto>();
            }

            var result = _mapper.Map<List<ApartmentDto>>(apartments);

            return result;
        }
    }
}
