using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using System.Security.Claims;
using RestaurantsReservation.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.Identity;

namespace RestaurantsReservation.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepo;
        private readonly IMapper _mapper;

        public ReservationController(IReservationRepository reservationRepo, IMapper mapper)
        {
            _reservationRepo = reservationRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
        {
            var reservations = await _reservationRepo.GetAllAsync();

            var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);

            return Ok(reservationsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            var reservation = await _reservationRepo.GetByIdAsync(id);

            if (reservation is null) return NotFound();

            var reservationDto = _mapper.Map<ReservationDto>(reservation);

            return Ok(reservationDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> CreateReservation(ReservationCreateDto reservationDto)
        {

            Validations.IsValidDate(reservationDto.ReservationDate, out bool isValidDate, out DateOnly reservationDate);

            if (!isValidDate) return BadRequest("Invalid Date Format");

            var todayDate = DateOnly.FromDateTime(DateTime.UtcNow);

            if (todayDate>reservationDate) return BadRequest("Invalid Date");

            Validations.IsValidTime(reservationDto.StartAt, out bool isValidStartTime, out TimeOnly startTime);

            if (!isValidStartTime) return BadRequest("Invalid Start Time Format");

            Validations.IsValidTime(reservationDto.EndAt, out bool isValidEndTime, out TimeOnly endTime);

            if (!isValidEndTime) return BadRequest("Invalid End Time Format");

            if (endTime < startTime) return BadRequest("Invalid Time Period");

            var username = GetUserName();

            var reservation = _mapper.Map<ReservationSchedule>(reservationDto);

            reservation.CreatedBy = username;

            await _reservationRepo.CreateAsync(reservation);

            var reservationToReturn= _mapper.Map<ReservationDto>(reservation);

            return Created($"/api/reservations/{reservation.Id}", reservationToReturn);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation(ReservationUpdateDto reservationUpdateDto, int id)
        {
            var reservation = await _reservationRepo.GetByIdAsync(id);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationRepo.GetByIdAsync(id);
            if (reservation is null) return NoContent();
            reservation.IsDeleted = true;
            await _reservationRepo.UpdateAsync(reservation);
            return NoContent();
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
}
