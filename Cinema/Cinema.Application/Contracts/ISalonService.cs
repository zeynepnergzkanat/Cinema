using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Application.Common;
using Cinema.Application.Dtos;

namespace Cinema.Application.Contracts;

public interface ISalonService
{
    Task<Result<IEnumerable<SalonDto>>> GetAllAsync();
    Task<Result<SalonDto>> CreateAsync(CreateSalonRequest request);
    Task<Result> DeleteAsync(Guid id);
}
