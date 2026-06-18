using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Identity;
using Cinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

public class BiletConfiguration : IEntityTypeConfiguration<Bilet>
{
    public void Configure(EntityTypeBuilder<Bilet> builder)
    {
        builder.ToTable("Biletler", "app");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasQueryFilter(x => x.IsActive);

        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.TotalPrice).IsRequired().HasColumnType("money");

        builder.HasOne(b => b.Seans)
            .WithMany(s => s.Biletler)
            .HasForeignKey(b => b.SeansId);

        builder.HasOne(b => b.User)
            .WithMany(u => u.Biletler)
            .HasForeignKey(b => b.UserId);
    }
}
