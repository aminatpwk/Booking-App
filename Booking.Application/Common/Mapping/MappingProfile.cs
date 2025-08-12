using AutoMapper;
using Booking.Application.Common.DTOs;
using Booking.Domain.Bookings;
using Booking.Domain.Owners;
using Booking.Domain.Photos;
using Booking.Domain.Reviews;
using Booking.Domain.Users;

namespace Booking.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookingDto, BookingEntity>().ForMember(dest => dest.ApartmentId, opt => opt.MapFrom(src => src.ApartmentId))
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.End))
                .ForMember(dest => dest.Apartment, opt => opt.MapFrom(src => src.Apartment))
                .ReverseMap();

            CreateMap<ApartmentDto, Apartment>()
                .ConstructUsing(dto => Apartment.Create(
                    dto.OwnerId,
                    dto.Name,
                    dto.Country,
                    dto.City,
                    dto.Address,
                    dto.Price,
                    dto.Description,
                    dto.CleaningFee,
                    dto.Bedrooms,
                    dto.Bathrooms,
                    dto.MaxGuests,
                    dto.Type,
                    dto.Amenities,
                    dto.IsActive,
                    dto.IsAvailable));
            CreateMap<Apartment, ApartmentDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Decription))
                .ForMember(dest => dest.ImagesBase64, opt => opt.MapFrom(src => src.Photos != null ? src.Photos.Select(p => p.ImageData != null ? Convert.ToBase64String(p.ImageData) : null).ToList() : new List<string>()));
            
            CreateMap<Photo, PhotoDto>()
                .ForMember(dest => dest.Base64Image,opt => opt.MapFrom(src => src.ImageData != null ? Convert.ToBase64String(src.ImageData) : null));
            
            CreateMap<UserDto, User>()
                .ConstructUsing(dto => User.CreateUser(dto.FirstName, dto.LastName, dto.Email, dto.Password))
                .ForMember(dest => dest.Password, opt => opt.Ignore());
            
            CreateMap<Review, ReviewDto>();
            
            //CreateMap<Owner, OwnerDto>()
            //    .ForMember(dest => dest.ApartmentId, opt => opt.MapFrom(src => src.Apartments.Select(a => a.Id).ToList()));

        }
    }
}
