using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Fluent Validation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            return services;
        }
    }
}