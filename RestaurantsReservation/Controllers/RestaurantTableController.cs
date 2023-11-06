using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.DTOs.RestaurantTableDtos;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using System.Security.Claims;

namespace RestaurantsReservation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/restaurantTables")]
[ApiController]
public class RestaurantTableController : ControllerBase
{
    private readonly IRestaurantTableRepository _restaurantTableRepo;
    private readonly IReservationRepository _reservationRepo;
    private readonly IMapper _mapper;

    public RestaurantTableController(IRestaurantTableRepository restaurantTableRepo,
        IReservationRepository reservationRepo,
        IMapper mapper)
    {
        _restaurantTableRepo = restaurantTableRepo;
        _reservationRepo = reservationRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantTableDto>>> GetAllRestaurantTables()
    {
        var restaurantTables = await _restaurantTableRepo.GetAllAsync();

        var restaurantTablesToReturn = _mapper.Map<IEnumerable<RestaurantTableDto>>(restaurantTables);

        return Ok(restaurantTablesToReturn);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantTableDto>> GetRestaurantTable(int id)
    {
        var restaurantTable = await _restaurantTableRepo.GetByIdAsync(id);

        if (restaurantTable is null) return NotFound();

        var restaurantTableToReturn = _mapper.Map<RestaurantTableDto>(restaurantTable);

        return Ok(restaurantTableToReturn);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<RestaurantTableDto>> CreateRestaurantTable(RestaurantTableCreateDto restaurantTableDto)
    {
        var username = GetUserName();
        bool flag = false;
        if (restaurantTableDto.RestaurantTableType.ToLower() == "indoor" 
            || restaurantTableDto.RestaurantTableType.ToLower() == "outdoor")
            flag = true;

        if (!flag) return BadRequest();  

        if (restaurantTableDto.RestaurantTableType.ToLower() == "indoor")
            restaurantTableDto.RestaurantTableType = "Indoor";
        else
            restaurantTableDto.RestaurantTableType = "Outdoor";

        var restaurantTable = _mapper.Map<RestaurantTable>(restaurantTableDto);
        restaurantTable.CreatedBy = username;
        await _restaurantTableRepo.CreateAsync(restaurantTable);

        var restaurantTableToReturn = _mapper.Map<RestaurantTableDto>(restaurantTable);

        return Created($"/api/restaurantTables/{restaurantTable.Id}", restaurantTableToReturn);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRestaurantTable(RestaurantTableUpdateDto restaurantTableUpdateDto, int id)
    {
        var username = GetUserName();

        var restTable = await _restaurantTableRepo.GetByIdAsync(id);

        if (restTable is null) return NotFound();

        var res = _mapper.Map(restaurantTableUpdateDto, restTable);

        res.UpdatedBy = username;

        res.LastUpdated = DateTime.UtcNow;

        await _restaurantTableRepo.UpdateAsync(res);

        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRestaurantTable(int id)
    {
        var restaurantTable = await _restaurantTableRepo.GetByIdAsync(id);

        if (restaurantTable is null) return NoContent();

        restaurantTable.IsDeleted = true;

        await _restaurantTableRepo.UpdateAsync(restaurantTable);

        return NoContent();
    }

    [HttpGet("{id}/reservations")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllTableReservations(int id)
    {
        var reservations = await _reservationRepo.GetUserReservationsAsync(id);

        var reservationsToReturn = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

        return Ok(reservationsToReturn);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost("{tableId}/reservations/{reservationId}")]
    public async Task<ActionResult<RestaurantTableDto>> AddReservationToTable(int tableId, int reservationId)
    {
        var table = await _restaurantTableRepo.GetByIdAsync(tableId);

        if (table is null) return BadRequest("Table Not Found");

        var reservation = await _reservationRepo.GetByIdAsync(reservationId);

        if (reservation is null) return BadRequest("Reservation Not Found");

        foreach (var res in table.Reservations)
            if (res.Id == reservationId) return BadRequest("Reservation Already Exists");

        table.Reservations.Add(reservation);

        await _restaurantTableRepo.UpdateAsync(table);

        var tableToReturn = _mapper.Map<RestaurantTableDto>(table);

        return Ok(tableToReturn);

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
