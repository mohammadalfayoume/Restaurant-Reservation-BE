using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.DTOs.UserDto;
using RestaurantsReservation.Helpers;
using RestaurantsReservation.Interfaces;

namespace RestaurantsReservation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/users")]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    private readonly IReservationRepository _reservationRepo;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepo,
        IReservationRepository reservationRepo,
        IMapper mapper) 
    {
        _userRepo = userRepo;
        _reservationRepo = reservationRepo;
        _mapper = mapper;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsers()
    {
        return Ok(await _userRepo.GetDtoUsersAsync());
        
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUserDto?>> GetUserById(int id)
    {
        var user= await _userRepo.GetDtoUserByIdAsync(id);
        if (user is null) return NotFound();
        return Ok(user);
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(UpdateUserDto updatedUserDto, int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user is null) return NotFound();
        user.LastUpdated= DateTime.Now;
        user.UserName = updatedUserDto.UserName is not null ? updatedUserDto.UserName : user.UserName;
        user.FirstName = updatedUserDto.FirstName is not null ? updatedUserDto.FirstName : user.FirstName;
        user.LastName = updatedUserDto.LastName is not null ? updatedUserDto.LastName : user.LastName;
        user.Email = updatedUserDto.Email is not null ? updatedUserDto.Email : user.Email;
        await _userRepo.UpdateAsync(user);
        return NoContent();
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user is not null)
        {
            user.IsDeleted = true;
            await _userRepo.UpdateAsync(user);
        }
        return NoContent();
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpGet("{id}/reservations")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllUserReservations(int id)
    {
        var reservations = await _reservationRepo.GetUserReservationsAsync(id);
        var reservationsToReturn = _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        return Ok(reservationsToReturn);
    }

    [HttpPost("{userId}/reservations/{reservationId}")]
    public async Task<ActionResult<AppUserDto>> ReserveRestaurantTable(int userId, int reservationId)
    {
        var user = await _userRepo.GetByIdAsync(userId);
        if (user is null) return BadRequest("User Not Found");
        var reservation = await _reservationRepo.GetByIdAsync(reservationId);
        if (reservation is null) return BadRequest("Reservation Schedule Not Found");
        if (reservation.IsReserved && !reservation.IsCanceled) return BadRequest("This Table Already Reserved");

        // Check if reservation dateTime is before today
        bool isOldDate = Validations.IsOldDate(reservation);

        if (isOldDate) return BadRequest("Reservation Date Invalid");

        foreach (var res in user.Reservations)
        {
            bool isValid = Validations.IsValidReservation(res, reservation.ReservationDate,
                 reservation.StartAt, reservation.EndAt);
            if (!isValid) return BadRequest("Conflict Found");
        }
        reservation.IsCanceled = false;
        reservation.IsReserved = true;
        user.Reservations.Add(reservation);
        await _userRepo.UpdateAsync(user);
        var userToReturn = _mapper.Map<AppUserDto>(user);
        return Ok(userToReturn);
        
    }

    
    
}
