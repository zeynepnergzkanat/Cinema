using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos;

public record BiletDto(
    Guid Id,
    string FilmTitle,
    string SalonName,
    DateTime StartTime,
    int Quantity,
    decimal TotalPrice,
    string BiletTipi,
    string KoltukNumaralari,
    DateTime CreatedAt
);