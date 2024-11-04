using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Service.Abstracts;
using ToDoApp.Service.Concretes;
using ToDoApp.Service.Profiles;
using ToDoApp.Service.Rules;

namespace ToDoApp.Service;

public static class ServiceDependecies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddScoped<ToDoBusinessRules>();
        services.AddScoped<CategoryBusinessRules>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IToDoService, ToDoService>();
        services.AddScoped<IUserService, UserService>();
        
        return services;
    }
}