using AutoMapper;
using Booking.Application.Common.DTOs;
using Booking.Application.Common.Model;
using Booking.Application.Common.Services;
using Booking.Domain.Apartments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Queries.GetAllPaged
{
    public class GetAllApartmentsPagedHandler : IRequestHandler<GetAllApartmentsPagedQuery, PagedResult<ApartmentDto>>
    {
        private readonly IApartmentRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private const string CacheKeyPrefix = "apartments_paged";
        private const int MaxPageSize = 100;
        private const int DefaultPageSize = 10;

        public GetAllApartmentsPagedHandler(IApartmentRepository repository, IMapper mapper, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<PagedResult<ApartmentDto>> Handle(GetAllApartmentsPagedQuery request, CancellationToken cancellationToken)
        {
            var normalizedRequest = NormalizeRequest(request);
            var cacheKey = GenerateCacheKey(normalizedRequest);
            return await _cacheService.GetAsync(cacheKey,
                async () => await FetchPagedApartments(normalizedRequest, cancellationToken),
                expiration: TimeSpan.FromMinutes(5));
        }

        #region private method

        private GetAllApartmentsPagedQuery NormalizeRequest(GetAllApartmentsPagedQuery request)
        {
            return new GetAllApartmentsPagedQuery
            {
                PageIndex = Math.Max(0, request.PageIndex),
                PageSize = Math.Clamp(request.PageSize <= 0 ? DefaultPageSize : request.PageSize, 1, MaxPageSize)
            };
        }

        private string GenerateCacheKey(GetAllApartmentsPagedQuery request)
        {
            var keyObject = new
            {
                request.PageIndex,
                request.PageSize,
                SearchTerm = request.SearchTerm?.ToLowerInvariant()?.Trim(),
                request.SortBy,
                request.SortDescending,
                Country = request.Country?.ToLowerInvariant()?.Trim(),
                City = request.City?.ToLowerInvariant()?.Trim(),
                request.Type,
                request.MinPrice,
                request.MaxPrice,
                StartDate = request.startDate?.ToString("yyyy-MM-dd"),
                EndDate = request.endDate?.ToString("yyyy-MM-dd")
            };

            var serialized = JsonSerializer.Serialize(keyObject);
            var hash = Convert.ToBase64String(
                System.Security.Cryptography.SHA256.HashData(
                    System.Text.Encoding.UTF8.GetBytes(serialized)
                )
            ).Substring(0, 16);

            return $"{CacheKeyPrefix}:{hash}";
        }

        private async Task<PagedResult<ApartmentDto>> FetchPagedApartments(GetAllApartmentsPagedQuery request, CancellationToken cancellationToken)
        {
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
                request.startDate,
                request.endDate,
                cancellationToken);

            var apartmentDtos = _mapper.Map<List<ApartmentDto>>(apartments);

            return new PagedResult<ApartmentDto>(
                apartmentDtos,
                request.PageIndex,
                request.PageSize,
                totalCount
            );
        }

        #endregion
    }
}
