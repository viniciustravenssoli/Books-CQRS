using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configurations)
        {
            var connectionString = configurations.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}