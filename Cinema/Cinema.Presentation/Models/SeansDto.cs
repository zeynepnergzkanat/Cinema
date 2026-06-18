namespace Cinema.Presentation.Models;

public class SeansDto
{
    public Guid Id { get; set; }
    public Guid FilmId { get; set; }
    public string FilmTitle { get; set; } = string.Empty;
    public Guid SalonId { get; set; }
    public string SalonName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public decimal Price { get; set; }
}