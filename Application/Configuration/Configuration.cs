using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Token;
using Application.Validations.Book;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Configuration
{
    public static class Configuration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITokenGeneratorDois, TokenGeneratorDois>();
            //Fluent Validation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining(typeof(CrateBookCommandValidator));
            services.AddHttpContextAccessor();


            return services;
        }
    }
}