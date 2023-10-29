using AutoMapper;
using RestaurantsReservation.DTOs.AccountDtos;
using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.DTOs.RestaurantDtos;
using RestaurantsReservation.DTOs.RestaurantTableDtos;
using RestaurantsReservation.DTOs.RestaurantTableTypeDtos;
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
        CreateMap<RestaurantTableTypeUpdateDto, RestaurantTableType>();
        CreateMap<ReservationUpdateDto, ReservationSchedule>();


        // CreateDto --> Entity
        CreateMap<RegisterDto, AppUser>();
        CreateMap<RestaurantCreateDto, Restaurant>();
        CreateMap<RestaurantTableCreateDto, RestaurantTable>();
        CreateMap<RestaurantTableTypeCreateDto, RestaurantTableType>();
        CreateMap<ReservationCreateDto, ReservationSchedule>();


        // Entity --> Dto
        CreateMap<AppUser, AppUserDto>();
        CreateMap<Restaurant, RestaurantDto>();
        CreateMap<RestaurantTable, RestaurantTableDto>();
        CreateMap<RestaurantTableType, RestaurantTableTypeDto>();
        CreateMap<ReservationSchedule, ReservationDto>();


        // Entity --> PlanDto
        CreateMap<AppUser, UserPlanDto>();
        CreateMap<Restaurant, RestaurantPlanDto>();
        CreateMap<RestaurantTable, RestaurnatTablePlanDto>();
        CreateMap<RestaurantTableType, RestaurantTableTypePlanDto>();
        CreateMap<ReservationSchedule, ReservationPlanDto>();
    }
}
