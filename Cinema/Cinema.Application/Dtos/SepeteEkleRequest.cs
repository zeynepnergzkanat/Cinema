using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Enums;

namespace Cinema.Application.Dtos;

public record SepeteEkleRequest(
    Guid SeansId,
    int Quantity,
    BiletTipi BiletTipi,
    string KoltukNumaralari
);