using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos;

public record UpdateFilmRequest(
    Guid Id,
    string Title,
    string Description,
    string ImageUrl,
    string Genre,
    int DurationMinutes,
    DateTime ReleaseDate
);
