using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Application.Common;
using Cinema.Application.Dtos;

namespace Cinema.Application.Contracts;


public interface IBiletService
{
    Task<Result<IEnumerable<BiletDto>>> GetByUserIdAsync(string userId);
    Task<Result> SatinAlAsync(string userId, OdemeRequest request);

    Task<Result<IEnumerable<BiletDto>>> GetBySeansIdAsync(Guid seansId);
}