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

public class SalonService(SalonRepository repository) : ISalonService
{
    public async Task<Result<IEnumerable<SalonDto>>> GetAllAsync()
    {
        try
        {
            var salons = await repository.GetListAsync();
            return Result<IEnumerable<SalonDto>>.Success(salons.Select(s => new SalonDto(s.Id, s.Name, s.Capacity, s.City)));
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<SalonDto>>.Failure(ex.Message);
        }
    }

    public async Task<Result<SalonDto>> CreateAsync(CreateSalonRequest request)
    {
        try
        {
            var salon = new Salon { Name = request.Name, Capacity = request.Capacity, City = request.City };
            await repository.AddAsync(salon);
            return Result<SalonDto>.Success(new SalonDto(salon.Id, salon.Name, salon.Capacity, salon.City));
        }
        catch (Exception ex)
        {
            return Result<SalonDto>.Failure(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        try
        {
            var salon = await repository.GetAsync(x => x.Id == id);
            if (salon is null)
                return Result.Failure("Salon bulunamadı.");
            await repository.DeleteAsync(salon);
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}