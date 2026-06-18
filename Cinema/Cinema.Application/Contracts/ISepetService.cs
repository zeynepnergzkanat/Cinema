using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Application.Common;
using Cinema.Application.Dtos;

namespace Cinema.Application.Contracts;

public interface ISepetService
{
    Task<Result<IEnumerable<SepetDto>>> GetByUserIdAsync(string userId);
    Task<Result> EkleAsync(string userId, SepeteEkleRequest request);
    Task<Result> SilAsync(Guid sepetId);
    Task<Result> TemizleAsync(string userId);
}
