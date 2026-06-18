namespace Cinema.Presentation.Models;

public record OdemeRequest(string CardHolderName, string CardNumber, string ExpiryDate, string Cvv);