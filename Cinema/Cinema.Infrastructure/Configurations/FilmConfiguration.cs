using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

public class FilmConfiguration : IEntityTypeConfiguration<Film>
{
    public void Configure(EntityTypeBuilder<Film> builder)
    {
        builder.ToTable("Films", "app");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasQueryFilter(x => x.IsActive);

        builder.Property(x => x.Title).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(1024);
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(512);
        builder.Property(x => x.Genre).IsRequired().HasMaxLength(128);
        builder.Property(x => x.DurationMinutes).IsRequired();
        builder.Property(x => x.ReleaseDate).IsRequired();
    }
}
