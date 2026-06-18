using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos;

public record SalonDto(
    Guid Id,
    string Name,
    int Capacity,
    string City
);