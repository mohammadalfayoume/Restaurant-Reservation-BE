using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.DTOs.RestaurantTableDtos;
using RestaurantsReservation.DTOs.RestaurantTableTypeDtos;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;

namespace RestaurantsReservation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("api/restaurantTableTypes")]
[ApiController]
public class RestaurantTableTypeController : ControllerBase
{
    private readonly IRestaurantTableTypeRepository _restaurantTableTypeRepo;
    private readonly IRestaurantTableRepository _restaurantTableRepo;
    private readonly IMapper _mapper;

    public RestaurantTableTypeController(IRestaurantTableTypeRepository restaurantTableTypeRepo,
        IRestaurantTableRepository restaurantTableRepository, IMapper mapper)
    {
        _restaurantTableTypeRepo = restaurantTableTypeRepo;
        _restaurantTableRepo = restaurantTableRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantTableTypeDto>>> GetAllRestaurantTableTypes()
    {
        var restaurantTableTypes = await _restaurantTableTypeRepo.GetAllAsync();

        var restaurantTableTypesToReturn = _mapper.Map<IEnumerable<RestaurantTableTypeDto>>(restaurantTableTypes);

        return Ok(restaurantTableTypesToReturn);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<RestaurantTableTypeDto>> GetRestaurantTableType(int id)
    {
        var restaurantTableType = await _restaurantTableTypeRepo.GetByIdAsync(id);

        if (restaurantTableType is null) return NotFound();

        var restaurantTableTypeToReturn = _mapper.Map<RestaurantTableTypeDto>(restaurantTableType);

        return Ok(restaurantTableTypeToReturn);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<RestaurantTableTypeDto>> CreateRestaurantTableType(RestaurantTableTypeCreateDto restaurantTableTypeDto)
    {
        var restaurantTableType = _mapper.Map<RestaurantTableType>(restaurantTableTypeDto);

        await _restaurantTableTypeRepo.CreateAsync(restaurantTableType);

        var restaurantTableTypeToReturn = _mapper.Map<RestaurantTableTypeDto>(restaurantTableType);

        return Created($"/api/restaurantTableTypes/{restaurantTableType.Id}", restaurantTableTypeToReturn);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRestaurantTableType(RestaurantTableTypeUpdateDto restaurantTableTypeUpdateDto, int id)
    {
        var restTableType = await _restaurantTableTypeRepo.GetByIdAsync(id);

        if (restTableType is null) return NotFound();

        var res = _mapper.Map(restaurantTableTypeUpdateDto, restTableType);

        await _restaurantTableTypeRepo.UpdateAsync(res);

        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRestaurantTableType(int id)
    {
        var restaurantTableType = await _restaurantTableTypeRepo.GetByIdAsync(id);

        if (restaurantTableType is null) return NoContent();

        restaurantTableType.IsDeleted = true;

        await _restaurantTableTypeRepo.UpdateAsync(restaurantTableType);

        return NoContent();
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [HttpPost("{typeId}/Table/{tableId}")]
    public async Task<ActionResult<RestaurantTableDto>> AddTypeToTable(int typeId, int tableId)
    {
        var resTable = await _restaurantTableRepo.GetByIdAsync(tableId);

        var resTableType = await _restaurantTableTypeRepo.GetByIdAsync(typeId);

        if (resTable is null || resTableType is null) return BadRequest();

        resTable.RestaurantTableType=resTableType;

        await _restaurantTableRepo.UpdateAsync(resTable);

        var tableToReturn = _mapper.Map<RestaurantTableDto>(resTable);

        return Ok(tableToReturn);
    }

}
