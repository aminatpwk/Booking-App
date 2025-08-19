using Booking.Application.Common.Model;
using Booking.Domain.Apartments;
using MediatR;
using Booking.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Queries.GetAllPaged
{
    public class GetAllApartmentsPagedQuery : IRequest<PagedResult<ApartmentDto>>
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } = "Price";
        public bool SortDescending { get; set; } = false;
        public string? SearchTerm { get; set; }

        public string? Country { get; set; }
        public string? City { get; set; }
        public ApartmentType? Type { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate   { get; set; }

    }
}
