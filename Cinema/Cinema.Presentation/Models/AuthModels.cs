namespace Cinema.Presentation.Models;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string FirstName, string LastName, string Email, string Password);
public record AuthResponse(string Id, string Email, string FirstName, string LastName, List<string> Roles, string Token);
public record UpdateProfileRequest(string FirstName, string LastName);
public record SifreDegistirRequest(string MevcutSifre, string YeniSifre);