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

public class SepetConfiguration : IEntityTypeConfiguration<Sepet>
{
    public void Configure(EntityTypeBuilder<Sepet> builder)
    {
        builder.ToTable("Sepetler", "app");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasQueryFilter(x => x.IsActive);

        builder.Property(x => x.Quantity).IsRequired();
        builder.Property(x => x.UnitPrice).IsRequired().HasColumnType("money");

        builder.HasOne(s => s.Seans)
            .WithMany()
            .HasForeignKey(s => s.SeansId);

        builder.HasOne(s => s.User)
            .WithMany(u => u.Sepetler)
            .HasForeignKey(s => s.UserId);
    }
}