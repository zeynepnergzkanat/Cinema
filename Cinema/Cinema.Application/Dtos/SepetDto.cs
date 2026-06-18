using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos;

public record SepetDto(
    Guid Id,
    Guid SeansId,
    string FilmTitle,
    string SalonName,
    DateTime StartTime,
    int Quantity,
    decimal UnitPrice,
    string BiletTipi,
    string KoltukNumaralari
);