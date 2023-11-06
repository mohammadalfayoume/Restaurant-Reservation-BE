using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.DTOs.UserDtos;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using Microsoft.AspNet.Identity;
using RestaurantsReservation.Helpers;

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
        var users = await _userRepo.GetAllAsync();
        return Ok(users);
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
        _mapper.Map(updatedUserDto, user);
        await _userRepo.UpdateAsync(user);
        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _userRepo.GetByIdAsync(id);
        if (user is null) return NoContent();
        user.IsDeleted = true;
        await _userRepo.UpdateAsync(user);
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
        try
        { 
            int currentUserId = GetCurrentUserIdFromToken();
            if (currentUserId != userId) return Forbid();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var user = await _userRepo.GetByIdAsync(userId);

        if (user is null) return BadRequest("User Not Found");

        var reservation = await _reservationRepo.GetByIdAsync(reservationId);

        if (reservation is null) return BadRequest("Reservation Schedule Not Found");

        if (reservation.IsReserved && !reservation.IsCanceled) return BadRequest("This Table Already Reserved");

        // Check if reservation dateTime is before today
        bool isOldDate = IsOldDate(reservation.ReservationDate, reservation.EndAt);

        if (isOldDate) return BadRequest("Reservation Date Invalid");

        foreach (var res in user.Reservations)
        {
            bool isValid = IsValidReservation(res, reservation.ReservationDate,
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

    [HttpPost("{userId}/reservations/{reservationId}/cancel")]
    public async Task<ActionResult<ReservationDto>> CancelReservation(int userId, int reservationId)
    {
        try
        {
            int currentUserId = GetCurrentUserIdFromToken();
            if (currentUserId != userId) return Forbid();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var username = GetUserName();

        var reservation = await _reservationRepo.GetByIdAsync(reservationId);

        if (reservation is null) return BadRequest();

        if (reservation.IsCanceled) return BadRequest("Reservation Already Canceled");

        bool canCancel = CanCancel(reservation);

        if (!canCancel) return BadRequest("Cannot Cancel The Reservation. You just can cancel before 2 hours of reservation start time");


        reservation.IsCanceled = true;

        reservation.IsReserved = false;

        reservation.UpdatedBy = username;

        reservation.User = null;

        await _reservationRepo.UpdateAsync(reservation);

        var reservationToReturn = _mapper.Map<ReservationDto>(reservation);

        return Ok(reservationToReturn);

    }

    [HttpPut("{userId}/reservations/{reservationId}")]
    public async Task<ActionResult> UpdateReservation(ReservationUpdateDto reservationUpdateDto, int userId,int reservationId)
    {
        try
        {
            int currentUserId = GetCurrentUserIdFromToken();
            if (currentUserId != userId) return Forbid();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        var reservation = await _reservationRepo.GetByIdAsync(reservationId);

        if (reservation is null) return NotFound();

        Validations.IsValidDate(reservationUpdateDto.ReservationDate, out bool isValidDate, out DateOnly reservationDate);

        if (!isValidDate) return BadRequest("Invalid Date Format");

        var todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
        if (todayDate > reservationDate) return BadRequest("Invalid Date");

        Validations.IsValidTime(reservationUpdateDto.StartAt, out bool isValidStartTime, out TimeOnly startTime);

        if (!isValidStartTime) return BadRequest("Invalid Start Time Format");

        Validations.IsValidTime(reservationUpdateDto.EndAt, out bool isValidEndTime, out TimeOnly endTime);

        if (!isValidEndTime) return BadRequest("Invalid End Time Format");

        if (endTime < startTime) return BadRequest("Invalid Time Period");

        var username = GetUserName();

        _mapper.Map(reservationUpdateDto, reservation);

        reservation.LastUpdated = DateTime.UtcNow;

        reservation.UpdatedBy = username;

        await _reservationRepo.UpdateAsync(reservation);

        return NoContent();

    }

    /// <summary>
    /// Check if user can reserve at given time.
    /// </summary>
    /// <param name="reservation"></param>
    /// <returns>Boolean</returns>
    private static bool CanCancel(ReservationSchedule reservation)
    {
        DateOnly todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
        TimeOnly todayTime = TimeOnly.FromDateTime(DateTime.UtcNow);
        var reservationDate = DateOnly.Parse(reservation.ReservationDate);
        var startAt = TimeOnly.Parse(reservation.StartAt);
        if (todayDate == reservationDate && startAt < todayTime.AddHours(2))
            return false;
        return true;
    }

    /// <summary>
    /// Check if the given date in the past.
    /// </summary>
    /// <param name="reservationDate"></param>
    /// <param name="startAt"></param>
    /// <returns>Boolean</returns>
    private static bool IsOldDate(string reservationDate, string startAt)
    {
        DateOnly todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
        TimeOnly todayTime = TimeOnly.FromDateTime(DateTime.UtcNow);

        var currentReservationDate = DateOnly.Parse(reservationDate);
        var currentStartAt = TimeOnly.Parse(startAt);
        if (currentReservationDate < todayDate) return true;

        return currentReservationDate == todayDate && currentStartAt < todayTime;
    }

    /// <summary>
    /// Check if the given reservation date and time is valid.
    /// </summary>
    /// <param name="reservationSchedule"></param>
    /// <param name="newReservationDate"></param>
    /// <param name="newStartAt"></param>
    /// <param name="newEndAt"></param>
    /// <returns>Boolean</returns>
    private static bool IsValidReservation(ReservationSchedule reservationSchedule, string newReservationDate, string newStartAt, string newEndAt)
    {

        var currentReservationDate = DateOnly.Parse(reservationSchedule.ReservationDate);
        var currentStartAt = TimeOnly.Parse(reservationSchedule.StartAt);
        var currentEndAt = TimeOnly.Parse(reservationSchedule.EndAt);

        var newReseDate = DateOnly.Parse(newReservationDate);
        var newResStartAt = TimeOnly.Parse(newStartAt);
        var newResEndAt = TimeOnly.Parse(newEndAt);

        if (currentReservationDate == newReseDate)
        {
            if ((newResStartAt < currentStartAt && newResEndAt <= currentStartAt) || (newResStartAt >= currentEndAt && newResEndAt > currentEndAt)) return true;
        }
        return true;

    }

    /// <summary>
    /// Get current user id from token.
    /// </summary>
    /// <returns>Integer</returns>
    /// <exception cref="ApplicationException"></exception>
    private int GetCurrentUserIdFromToken()
    {
        var identity = HttpContext.User.Identity;

        if (identity == null) throw new ApplicationException("Unable to retrieve the current user ID from the token.");

        var userIdClaim = identity.GetUserId();

        if (userIdClaim != null && int.TryParse(userIdClaim, out int userId))
        {
            return userId;
        }
        // Handle the case when the user ID cannot be retrieved from the token
        throw new ApplicationException("Unable to retrieve the current user ID from the token.");
    }

    /// <summary>
    /// Get the username from Name claim
    /// </summary>
    /// <returns>String</returns>
    private string? GetUserName()
    {
        var identity = HttpContext.User.Identity;

        if (identity == null) throw new ApplicationException("Unable to retrieve the current userName from the token.");

        var userNameClaim = identity.GetUserName();

        if (userNameClaim is not null) return userNameClaim;

        throw new ApplicationException("Unable to retrieve the current userName from the token.");
    }
}
