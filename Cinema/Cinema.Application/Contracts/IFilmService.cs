using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Application.Common;
using Cinema.Application.Dtos;

namespace Cinema.Application.Contracts;

public interface IFilmService
{
    Task<Result<IEnumerable<FilmDto>>> GetAllAsync();
    Task<Result<FilmDto>> GetByIdAsync(Guid id);
    Task<Result<FilmDto>> CreateAsync(CreateFilmRequest request);
    Task<Result> UpdateAsync(UpdateFilmRequest request);
    Task<Result> DeleteAsync(Guid id);
}