using RestaurantsReservation.DTOs.RestaurantTableDtos;
using RestaurantsReservation.DTOs.UserDtos;

namespace RestaurantsReservation.DTOs.ReservationDtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string ReservationDate { get; set; }
        public string StartAt { get; set; }
        public string EndAt { get; set; }
        public bool IsReserved { get; set; }
        public bool IsCanceled { get; set; }
        public UserPlanDto? User { get; set; }
        public RestaurnatTablePlanDto? RestaurantTable { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime Created { get; set; }
        public DateTime? LastUpdated { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
