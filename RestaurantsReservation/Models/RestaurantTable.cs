namespace RestaurantsReservation.Models;

public class RestaurantTable
{
    public int Id { get; set; }
    public RestaurantTableType? RestaurantTableType { get; set; }
    public int SeatingCapacity { get; set; }
    public int TableNumber { get; set; }
    public decimal Price { get; set; }
    public bool IsLoungeTable { get; set; }
    public string? Description { get; set; }
    public ICollection<ReservationSchedule> Reservations { get; set; }= new List<ReservationSchedule>();
    public Restaurant? Restaurant { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime Created { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
}