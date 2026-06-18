using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Application.Dtos;

public record OdemeRequest(
    string CardHolderName,
    string CardNumber,
    string ExpiryDate,
    string Cvv
);
