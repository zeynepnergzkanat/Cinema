using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Application.Common;
using Cinema.Application.Contracts;
using Cinema.Application.Dtos;
using Cinema.Domain.Entities;
using Cinema.Domain.Enums;
using Cinema.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Application.Features;

public class SepetService(SepetRepository repository, SeansRepository seansRepo) : ISepetService
{
    public async Task<Result<IEnumerable<SepetDto>>> GetByUserIdAsync(string userId)
    {
        try
        {
            var sepetler = await repository.GetListAsync(
                predicate: x => x.UserId == userId,
                include: q => q.Include(s => s.Seans).ThenInclude(s => s!.Film)
                               .Include(s => s.Seans).ThenInclude(s => s!.Salon)
            );
            return Result<IEnumerable<SepetDto>>.Success(sepetler.Select(s => new SepetDto(
                s.Id, s.SeansId, s.Seans!.Film!.Title, s.Seans!.Salon!.Name,
                s.Seans!.StartTime, s.Quantity, s.UnitPrice, s.BiletTipi.ToString(), s.KoltukNumaralari
            )));
        }
        catch (Exception ex) { return Result<IEnumerable<SepetDto>>.Failure(ex.Message); }
    }

    public async Task<Result> EkleAsync(string userId, SepeteEkleRequest request)
    {
        try
        {
            var seans = await seansRepo.GetAsync(x => x.Id == request.SeansId);
            if (seans is null) return Result.Failure("Seans bulunamadı.");

            decimal fiyat = request.BiletTipi == BiletTipi.Ogrenci
                ? seans.Price * 0.8m
                : seans.Price;

            var sepet = new Sepet
            {
                UserId = userId,
                SeansId = request.SeansId,
                Quantity = request.Quantity,
                UnitPrice = fiyat,
                BiletTipi = request.BiletTipi,
                KoltukNumaralari = request.KoltukNumaralari
            };
            await repository.AddAsync(sepet);
            return Result.Success();
        }
        catch (Exception ex) { return Result.Failure(ex.Message); }
    }

    public async Task<Result> SilAsync(Guid sepetId)
    {
        try
        {
            var sepet = await repository.GetAsync(x => x.Id == sepetId);
            if (sepet is null) return Result.Failure("Sepet öğesi bulunamadı.");
            await repository.DeleteAsync(sepet);
            return Result.Success();
        }
        catch (Exception ex) { return Result.Failure(ex.Message); }
    }

    public async Task<Result> TemizleAsync(string userId)
    {
        try
        {
            var sepetler = await repository.GetListAsync(x => x.UserId == userId);
            foreach (var sepet in sepetler)
                await repository.DeleteAsync(sepet);
            return Result.Success();
        }
        catch (Exception ex) { return Result.Failure(ex.Message); }
    }
}