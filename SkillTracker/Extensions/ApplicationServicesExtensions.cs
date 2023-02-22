using Domain.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace SkillTracker.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IQuestionsService, QuestionsService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            return services;
        }
    }
}
