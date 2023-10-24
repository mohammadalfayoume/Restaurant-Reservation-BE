using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantsReservation.DTOs.ReservationDtos;
using RestaurantsReservation.Interfaces;
using RestaurantsReservation.Models;
using System.Security.Claims;
using RestaurantsReservation.Helpers;

namespace RestaurantsReservation.Controllers
{
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
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> CreateReservation(ReservationCreateDto reservationDto)
        {

            ValidationDateTime.IsValidDate(reservationDto.ReservationDate, out bool isValidDate, out DateOnly reservationDate);

            if (!isValidDate) return BadRequest("Invalid Date Format");

            var todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
            if (todayDate>reservationDate) return BadRequest("Invalid Date");

            ValidationDateTime.IsValidTime(reservationDto.StartAt, out bool isValidStartTime, out TimeOnly startTime);

            if (!isValidStartTime) return BadRequest("Invalid Start Time Format");

            ValidationDateTime.IsValidTime(reservationDto.EndAt, out bool isValidEndTime, out TimeOnly endTime);

            if (!isValidEndTime) return BadRequest("Invalid End Time Format");

            if (endTime < startTime) return BadRequest("Invalid Time Period");

            var username = GetUserName();

            var reservation = _mapper.Map<ReservationSchedule>(reservationDto);

            reservation.Created = DateTime.UtcNow;

            reservation.CreatedBy = username;

            await _reservationRepo.CreateAsync(reservation);

            var reservationToReturn= _mapper.Map<ReservationDto>(reservation);

            return Created($"/api/reservations/{reservation.Id}", reservationToReturn);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation(ReservationUpdateDto reservationUpdateDto, int id)
        {
            var reservation = await _reservationRepo.GetByIdAsync(id);

            if (reservation is null) return NotFound();

            ValidationDateTime.IsValidDate(reservationUpdateDto.ReservationDate, out bool isValidDate, out DateOnly reservationDate);

            if (!isValidDate) return BadRequest("Invalid Date Format");

            var todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
            if (todayDate > reservationDate) return BadRequest("Invalid Date");

            ValidationDateTime.IsValidTime(reservationUpdateDto.StartAt, out bool isValidStartTime, out TimeOnly startTime);

            if (!isValidStartTime) return BadRequest("Invalid Start Time Format");

            ValidationDateTime.IsValidTime(reservationUpdateDto.EndAt, out bool isValidEndTime, out TimeOnly endTime);

            if (!isValidEndTime) return BadRequest("Invalid End Time Format");

            if (endTime < startTime) return BadRequest("Invalid Time Period");

            var username = GetUserName();

            _mapper.Map(reservationUpdateDto, reservation);

            reservation.LastUpdated = DateTime.UtcNow;

            reservation.UpdatedBy = username;

            await _reservationRepo.UpdateAsync(reservation);

            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var reservation = await _reservationRepo.GetByIdAsync(id);
            if (reservation is not null)
            {
                reservation.IsDeleted = true;
                await _reservationRepo.UpdateAsync(reservation);
            }
            return NoContent();
        }
        [HttpPost("{id}/cancel")]
        public async Task<ActionResult<ReservationDto>> CancelReservation(int id)
        {
            var username = GetUserName();
            var reservation = await _reservationRepo.GetByIdAsync(id);
            if (reservation is null) return BadRequest();
            if (reservation.IsCanceled) return BadRequest("Already Reservation Canceled");
            bool canCancel = CanCancel(reservation);
            if (!canCancel)
                return BadRequest("Cannot Cancel The Reservation. You just can cancel before 2 hours of reservation start time");


            reservation.IsCanceled= true;
            reservation.IsReserved = false;
            reservation.UpdatedBy= username;
            reservation.User = null;
            await _reservationRepo.UpdateAsync(reservation);
            var reservationToReturn = _mapper.Map<ReservationDto>(reservation);
            return Ok(reservationToReturn);
            
        }

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

        private string? GetUserName()
        {
            return User.FindFirst(ClaimTypes.Name)?.Value;
        }
        
    }
}
