namespace Cinema.Presentation.Models;

public class SalonDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string City { get; set; } = string.Empty;
}