using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

public class SeansConfiguration : IEntityTypeConfiguration<Seans>
{
    public void Configure(EntityTypeBuilder<Seans> builder)
    {
        builder.ToTable("Seanslar", "app");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasQueryFilter(x => x.IsActive);

        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.Price).IsRequired().HasColumnType("money");

        builder.HasOne(s => s.Film)
            .WithMany(f => f.Seanslar)
            .HasForeignKey(s => s.FilmId);

        builder.HasOne(s => s.Salon)
            .WithMany(sa => sa.Seanslar)
            .HasForeignKey(s => s.SalonId);
    }
}