namespace RestaurantsReservation.Models;
public class BaseModel
{
    public int Id { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdated { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set;}
    public bool IsDeleted { get; set; } = false;

}
