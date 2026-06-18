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

public class BiletService(BiletRepository biletRepo, SepetRepository sepetRepo) : IBiletService
{
    public async Task<Result<IEnumerable<BiletDto>>> GetByUserIdAsync(string userId)
    {
        try
        {
            var biletler = await biletRepo.GetListAsync(
                predicate: x => x.UserId == userId,
                include: q => q.Include(b => b.Seans).ThenInclude(s => s!.Film)
                               .Include(b => b.Seans).ThenInclude(s => s!.Salon)
            );
            return Result<IEnumerable<BiletDto>>.Success(biletler.Select(b => new BiletDto(
                b.Id, b.Seans!.Film!.Title, b.Seans!.Salon!.Name, b.Seans!.StartTime,
                b.Quantity, b.TotalPrice, b.BiletTipi.ToString(), b.KoltukNumaralari, b.CreatedAt
            )));
        }
        catch (Exception ex) { return Result<IEnumerable<BiletDto>>.Failure(ex.Message); }
    }

    public async Task<Result<IEnumerable<BiletDto>>> GetBySeansIdAsync(Guid seansId)
    {
        try
        {
            var biletler = await biletRepo.GetListAsync(
                predicate: x => x.SeansId == seansId,
                include: q => q.Include(b => b.Seans).ThenInclude(s => s!.Film)
                               .Include(b => b.Seans).ThenInclude(s => s!.Salon)
            );
            return Result<IEnumerable<BiletDto>>.Success(biletler.Select(b => new BiletDto(
                b.Id, b.Seans!.Film!.Title, b.Seans!.Salon!.Name, b.Seans!.StartTime,
                b.Quantity, b.TotalPrice, b.BiletTipi.ToString(), b.KoltukNumaralari, b.CreatedAt
            )));
        }
        catch (Exception ex) { return Result<IEnumerable<BiletDto>>.Failure(ex.Message); }
    }

    public async Task<Result> SatinAlAsync(string userId, OdemeRequest request)
    {
        try
        {
            var sepetler = await sepetRepo.GetListAsync(
                predicate: x => x.UserId == userId,
                include: q => q.Include(s => s.Seans)
            );

            if (!sepetler.Any()) return Result.Failure("Sepetiniz boş.");

            foreach (var sepet in sepetler)
            {
                var bilet = new Bilet
                {
                    UserId = userId,
                    SeansId = sepet.SeansId,
                    Quantity = sepet.Quantity,
                    TotalPrice = sepet.Quantity * sepet.UnitPrice,
                    BiletTipi = sepet.BiletTipi,
                    KoltukNumaralari = sepet.KoltukNumaralari
                };
                await biletRepo.AddAsync(bilet);
                await sepetRepo.DeleteAsync(sepet);
            }
            return Result.Success();
        }
        catch (Exception ex) { return Result.Failure(ex.Message); }
    }
}