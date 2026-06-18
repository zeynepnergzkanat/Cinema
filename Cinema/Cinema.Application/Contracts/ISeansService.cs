using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Application.Common;
using Cinema.Application.Dtos;

namespace Cinema.Application.Contracts;

public interface ISeansService
{
    Task<Result<IEnumerable<SeansDto>>> GetAllAsync();
    Task<Result<IEnumerable<SeansDto>>> GetByFilmIdAsync(Guid filmId);
    Task<Result<SeansDto>> GetByIdAsync(Guid id);
    Task<Result<SeansDto>> CreateAsync(CreateSeansRequest request);
    Task<Result> DeleteAsync(Guid id);
}
