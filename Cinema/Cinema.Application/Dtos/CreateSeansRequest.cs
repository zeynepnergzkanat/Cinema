using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos;

public record CreateSeansRequest(
    Guid FilmId,
    Guid SalonId,
    DateTime StartTime,
    decimal Price
);