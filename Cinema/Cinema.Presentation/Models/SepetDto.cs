namespace Cinema.Presentation.Models;

public class SepetDto
{
    public Guid Id { get; set; }
    public Guid SeansId { get; set; }
    public string FilmTitle { get; set; } = string.Empty;
    public string SalonName { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string BiletTipi { get; set; } = string.Empty;
    public string KoltukNumaralari { get; set; } = string.Empty;
}