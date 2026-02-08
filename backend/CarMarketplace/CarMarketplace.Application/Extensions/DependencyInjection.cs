using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Validators;
using CarMarketplace.Application.Users.Factories;
using FluentValidation;

namespace CarMarketplace.Application.Extensions;

using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserRequest).Assembly));
        services.AddValidatorsFromAssemblyContaining(typeof(RegisterUserCommandValidator));

        // Validators
        services.AddScoped<IRegisterUserValidator, RegisterUserValidator>();

        // Factories
        services.AddScoped<IUserFactory, UserFactory>();
    }
}