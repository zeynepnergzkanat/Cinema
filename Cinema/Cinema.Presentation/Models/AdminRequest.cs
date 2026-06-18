namespace Cinema.Presentation.Models;

public record CreateFilmRequest(string Title, string Description, string ImageUrl, string Genre, int DurationMinutes, DateTime ReleaseDate);
public record UpdateFilmRequest(Guid Id, string Title, string Description, string ImageUrl, string Genre, int DurationMinutes, DateTime ReleaseDate);
public record CreateSeansRequest(Guid FilmId, Guid SalonId, DateTime StartTime, decimal Price);
public record CreateSalonRequest(string Name, int Capacity, string City);