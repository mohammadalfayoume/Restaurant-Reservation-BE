using AutoMapper;
using RestaurantsReservation.DTOs.AccountDtos;
using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.DTOs.RestaurantDtos;
using RestaurantsReservation.DTOs.RestaurantTableDtos;
using RestaurantsReservation.DTOs.UserDtos;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Helpers;

public class AutoMaperProfiles : Profile
{
    public AutoMaperProfiles() 
    {

        // UpdateDto --> Entity
        CreateMap<UpdateUserDto, AppUser>();
        CreateMap<RestaurantUpdateDto, Restaurant>();
        CreateMap<RestaurantTableUpdateDto, RestaurantTable>();
        CreateMap<ReservationUpdateDto, ReservationSchedule>();


        // CreateDto --> Entity
        CreateMap<RegisterDto, AppUser>();
        CreateMap<RestaurantCreateDto, Restaurant>();
        CreateMap<RestaurantTableCreateDto, RestaurantTable>();
        CreateMap<ReservationCreateDto, ReservationSchedule>();


        // Entity --> Dto
        CreateMap<AppUser, AppUserDto>();
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<RestaurantTable, RestaurantTableDto>();
        CreateMap<ReservationSchedule, ReservationDto>();


        // Entity --> PlanDto
        CreateMap<AppUser, UserPlanDto>();
        CreateMap<Restaurant, RestaurantPlanDto>();
        CreateMap<RestaurantTable, RestaurnatTablePlanDto>();
        CreateMap<ReservationSchedule, ReservationPlanDto>();
    }
}
