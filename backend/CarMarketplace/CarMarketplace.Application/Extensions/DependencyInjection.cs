using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Validators;
using CarMarketplace.Application.Common.Behaviors;
using CarMarketplace.Application.Users.Factories;
using FluentValidation;
using MediatR;

namespace CarMarketplace.Application.Extensions;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserRequest).Assembly));
        services.AddValidatorsFromAssemblyContaining(typeof(RegisterUserCommandValidator));

        // Pipeline
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

        // Validators
        services.AddScoped<IRegisterUserValidator, RegisterUserValidator>();

        // Factories
        services.AddScoped<IUserFactory, UserFactory>();
    }
}