using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Cinema.Application.Contracts;
using Cinema.Application.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IFilmService, FilmService>();
        services.AddScoped<ISeansService, SeansService>();
        services.AddScoped<ISepetService, SepetService>();
        services.AddScoped<IBiletService, BiletService>();
        services.AddScoped<ISalonService, SalonService>();

        return services;
    }
}