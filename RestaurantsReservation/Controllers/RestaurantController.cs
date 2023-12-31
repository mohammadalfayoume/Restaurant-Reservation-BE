﻿using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.DTOs.RestaurantDtos;
using RestaurantsReservation.DTOs.RestaurantTableDtos;
using RestaurantsReservation.Helpers;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using System.Security.Claims;

namespace RestaurantsReservation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/restaurants")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepo;
    private readonly IRestaurantTableRepository _restaurantTableRepo;
    private readonly IMapper _mapper;

    public RestaurantController(IRestaurantRepository restaurantRepo,
        IRestaurantTableRepository restaurantTableRepo,
        IMapper mapper)
    {
        _restaurantRepo = restaurantRepo;
        _restaurantTableRepo = restaurantTableRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await _restaurantRepo.GetAllAsync();
        var restaurantsDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return Ok(restaurantsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurantById(int id)
    {
        var restaurant =  await _restaurantRepo.GetByIdAsync(id);
        if (restaurant is null) return NotFound();
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
        return Ok(restaurantDto);
    }

    [HttpGet("search")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurant(string name)
    {
        var restaurants= await _restaurantRepo.GetRestaurantByNameAsync(name);
        var restaurantsToReturn = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return Ok(restaurantsToReturn);
    }

    [HttpGet("{id}/tables")]
    public async Task<ActionResult<IEnumerable<RestaurantTableDto>>> GetAllRestaurantTables(int id)
    {
        var tables = await _restaurantTableRepo.GetRestaurantTablesAsync(id);
        var tablesToReturn = _mapper.Map<IEnumerable<RestaurantTableDto>>(tables);
        return Ok(tablesToReturn);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<RestaurantDto>> CreateRestaurant(RestaurantCreateDto restaurantDto)
    {
        Validations.IsValidTime(restaurantDto.OpenAt, out bool isValidStartTime, out TimeOnly startTime);
        if (!isValidStartTime) return BadRequest("Invalid Open Time Format");

        Validations.IsValidTime(restaurantDto.CloseAt, out bool isValidEndTime, out TimeOnly endTime);

        if (!isValidEndTime) return BadRequest("Invalid Close Time Format");

        if (endTime < startTime) return BadRequest("Invalid Time Period");

        var username = GetUserName();

        var restaurant = _mapper.Map<Restaurant>(restaurantDto);

        restaurant.CreatedBy = username;

        await _restaurantRepo.CreateAsync(restaurant);

        var resToReturn = _mapper.Map<RestaurantDto>(restaurant);

        return Created($"/api/restaurants/{restaurant.Id}", resToReturn);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRestaurant(RestaurantUpdateDto restaurantDto, int id)
    {
        var username = GetUserName();

        var restaurant = await _restaurantRepo.GetByIdAsync(id);

        if (restaurant is null) return NotFound();

        var res = _mapper.Map(restaurantDto, restaurant);

        res.LastUpdated = DateTime.UtcNow;

        res.UpdatedBy = username;

        await _restaurantRepo.UpdateAsync(res);

        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRestaurant(int id)
    {
        var restaurant = await _restaurantRepo.GetByIdAsync(id);

        if (restaurant is null) return NoContent();

        restaurant.IsDeleted = true;

        await _restaurantRepo.UpdateAsync(restaurant);

        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost("{restaurantId}/tables/{tableId}")]
    public async Task<ActionResult<RestaurantDto>> AddTableToRestaurant(int restaurantId, int tableId)
    {
        var restaurant = await _restaurantRepo.GetByIdAsync(restaurantId);

        var table = await _restaurantTableRepo.GetByIdAsync(tableId);

        if (restaurant is null || table is null) return BadRequest();

        restaurant.Tables.Add(table);

        await _restaurantRepo.UpdateAsync(restaurant);

        var resToRet = _mapper.Map<RestaurantDto>(restaurant);

        return Ok(resToRet);
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
