namespace Cinema.Presentation.Models;

public class BiletDto
{
    public Guid Id { get; set; }
    public string FilmTitle { get; set; } = string.Empty;
    public string SalonName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string BiletTipi { get; set; } = string.Empty;
    public string KoltukNumaralari { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}