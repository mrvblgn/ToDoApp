using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.DataAccess.Abstracts;
using ToDoApp.DataAccess.Concretes;
using ToDoApp.DataAccess.Contexts;

namespace ToDoApp.DataAccess;

public static class DataAccessRepositoryDependencies
{
    public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IToDoRepository, EfToDoRepository>();
        services.AddScoped<ICategoryRepository, EfCategoryRepository>();

        services.AddDbContext<BaseDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        return services;
    }
}