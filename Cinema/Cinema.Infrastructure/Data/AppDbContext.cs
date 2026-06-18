using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Domain.Identity;
using Cinema.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    protected AppDbContext() { }

    public DbSet<Film> Films { get; set; }
    public DbSet<Salon> Salons { get; set; }
    public DbSet<Seans> Seanslar { get; set; }
    public DbSet<Bilet> Biletler { get; set; }
    public DbSet<Sepet> Sepetler { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<AppUser>().ToTable("Users", "auth");
        builder.Entity<IdentityRole>().ToTable("Roles", "auth");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "auth");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "auth");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "auth");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "auth");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "auth");
    }
}
