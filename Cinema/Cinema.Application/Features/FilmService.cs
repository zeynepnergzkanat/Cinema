using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Application.Common;
using Cinema.Application.Contracts;
using Cinema.Application.Dtos;
using Cinema.Domain.Entities;
using Cinema.Infrastructure.Repositories;

namespace Cinema.Application.Features;

public class FilmService(FilmRepository repository) : IFilmService
{
    public async Task<Result<IEnumerable<FilmDto>>> GetAllAsync()
    {
        try
        {
            var films = await repository.GetListAsync();
            return Result<IEnumerable<FilmDto>>.Success(films.Select(f => new FilmDto(
                f.Id, f.Title, f.Description, f.ImageUrl, f.Genre, f.DurationMinutes, f.ReleaseDate
            )));
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<FilmDto>>.Failure(ex.Message);
        }
    }

    public async Task<Result<FilmDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var film = await repository.GetAsync(x => x.Id == id);
            if (film is null)
                return Result<FilmDto>.Failure("Film bulunamadı.");
            return Result<FilmDto>.Success(new FilmDto(
                film.Id, film.Title, film.Description, film.ImageUrl, film.Genre, film.DurationMinutes, film.ReleaseDate
            ));
        }
        catch (Exception ex)
        {
            return Result<FilmDto>.Failure(ex.Message);
        }
    }

    public async Task<Result<FilmDto>> CreateAsync(CreateFilmRequest request)
    {
        try
        {
            var film = new Film
            {
                Title = request.Title,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Genre = request.Genre,
                DurationMinutes = request.DurationMinutes,
                ReleaseDate = request.ReleaseDate
            };
            await repository.AddAsync(film);
            return Result<FilmDto>.Success(new FilmDto(
                film.Id, film.Title, film.Description, film.ImageUrl, film.Genre, film.DurationMinutes, film.ReleaseDate
            ));
        }
        catch (Exception ex)
        {
            return Result<FilmDto>.Failure(ex.Message);
        }
    }

    public async Task<Result> UpdateAsync(UpdateFilmRequest request)
    {
        try
        {
            var film = await repository.GetAsync(x => x.Id == request.Id);
            if (film is null)
                return Result.Failure("Film bulunamadı.");

            film.Title = request.Title;
            film.Description = request.Description;
            film.ImageUrl = request.ImageUrl;
            film.Genre = request.Genre;
            film.DurationMinutes = request.DurationMinutes;
            film.ReleaseDate = request.ReleaseDate;

            await repository.UpdateAsync(film);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        try
        {
            var film = await repository.GetAsync(x => x.Id == id);
            if (film is null)
                return Result.Failure("Film bulunamadı.");
            await repository.DeleteAsync(film);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}
