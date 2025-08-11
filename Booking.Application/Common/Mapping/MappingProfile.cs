using AutoMapper;
using Booking.Application.Common.DTOs;
using Booking.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingDto, BookingEntity>().ForMember(dest => dest.ApartmentId, opt => opt.MapFrom(src => src.ApartmentId))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End));
        }
    }
}
