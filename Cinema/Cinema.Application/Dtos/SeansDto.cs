using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos;

public record SeansDto(
    Guid Id,
    Guid FilmId,
    string FilmTitle,
    Guid SalonId,
    string SalonName,
    string City,
    DateTime StartTime,
    decimal Price
);