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
using Microsoft.EntityFrameworkCore;

namespace Cinema.Application.Features;

public class SeansService(SeansRepository seansRepo, SalonRepository salonRepo, FilmRepository filmRepo) : ISeansService
{
    public async Task<Result<IEnumerable<SeansDto>>> GetAllAsync()
    {
        try
        {
            var seanslar = await seansRepo.GetListAsync(
                include: q => q.Include(s => s.Film).Include(s => s.Salon)
            );
            return Result<IEnumerable<SeansDto>>.Success(seanslar.Select(s => new SeansDto(
                s.Id, s.FilmId, s.Film!.Title, s.SalonId, s.Salon!.Name, s.Salon!.City, s.StartTime, s.Price
            )));
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<SeansDto>>.Failure(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<SeansDto>>> GetByFilmIdAsync(Guid filmId)
    {
        try
        {
            var seanslar = await seansRepo.GetListAsync(
                predicate: x => x.FilmId == filmId,
                include: q => q.Include(s => s.Film).Include(s => s.Salon)
            );
            return Result<IEnumerable<SeansDto>>.Success(seanslar.Select(s => new SeansDto(
                s.Id, s.FilmId, s.Film!.Title, s.SalonId, s.Salon!.Name, s.Salon!.City, s.StartTime, s.Price
            )));
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<SeansDto>>.Failure(ex.Message);
        }
    }

    public async Task<Result<SeansDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var seans = await seansRepo.GetAsync(
                predicate: x => x.Id == id,
                include: q => q.Include(s => s.Film).Include(s => s.Salon)
            );
            if (seans is null)
                return Result<SeansDto>.Failure("Seans bulunamadı.");
            return Result<SeansDto>.Success(new SeansDto(
                seans.Id, seans.FilmId, seans.Film!.Title, seans.SalonId, seans.Salon!.Name, seans.Salon!.City, seans.StartTime, seans.Price
            ));
        }
        catch (Exception ex)
        {
            return Result<SeansDto>.Failure(ex.Message);
        }
    }

    public async Task<Result<SeansDto>> CreateAsync(CreateSeansRequest request)
    {
        try
        {
            var film = await filmRepo.GetAsync(x => x.Id == request.FilmId);
            if (film is null)
                return Result<SeansDto>.Failure("Film bulunamadı.");

            var salon = await salonRepo.GetAsync(x => x.Id == request.SalonId);
            if (salon is null)
                return Result<SeansDto>.Failure("Salon bulunamadı.");

            var seans = new Seans
            {
                FilmId = request.FilmId,
                SalonId = request.SalonId,
                StartTime = request.StartTime,
                Price = request.Price
            };
            await seansRepo.AddAsync(seans);
            return Result<SeansDto>.Success(new SeansDto(
                seans.Id, seans.FilmId, film.Title, seans.SalonId, salon.Name, salon.City, seans.StartTime, seans.Price
            ));
        }
        catch (Exception ex)
        {
            return Result<SeansDto>.Failure(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        try
        {
            var seans = await seansRepo.GetAsync(x => x.Id == id);
            if (seans is null)
                return Result.Failure("Seans bulunamadı.");
            await seansRepo.DeleteAsync(seans);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}
