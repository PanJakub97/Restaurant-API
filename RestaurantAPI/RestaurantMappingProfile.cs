using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            //rest of properties is mapped automatically by AutoMapper due to the SAME NAMING between Restaurant and RestaurantDto

            CreateMap<Dish, DishDto>();

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address,
                c => c.MapFrom(dto => new Address() 
                {City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street}));

            CreateMap<CreateDishDto, Dish>();
        }
    }
}
