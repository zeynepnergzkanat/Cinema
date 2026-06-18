using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DbConn"));
        });

        services.AddScoped<FilmRepository>();
        services.AddScoped<SalonRepository>();
        services.AddScoped<SeansRepository>();
        services.AddScoped<BiletRepository>();
        services.AddScoped<SepetRepository>();

        return services;
    }
}
