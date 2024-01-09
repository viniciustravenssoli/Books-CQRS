using Domain.Interfaces;
using Infra.Repositories;
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


            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}