using CarMarketplace.Application.Authorization.Commands.RegisterUser;
using CarMarketplace.Application.Authorization.Helpers;
using CarMarketplace.Application.Authorization.Validators;
using CarMarketplace.Application.Cars.Factories;
using CarMarketplace.Application.Cars.Helpers;
using CarMarketplace.Application.Cars.Searchers;
using CarMarketplace.Application.Common.Behaviors;
using CarMarketplace.Application.Contacts.Factories;
using CarMarketplace.Application.Contacts.Helpers;
using CarMarketplace.Application.Contacts.Searchers;
using CarMarketplace.Application.Contacts.Validators;
using CarMarketplace.Application.Listings.Factories;
using CarMarketplace.Application.Listings.Helpers;
using CarMarketplace.Application.Listings.Searchers;
using CarMarketplace.Application.Users.Factories;
using CarMarketplace.Application.Users.Helpers;
using CarMarketplace.Application.Users.Searchers;
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
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));

        // Validators
        services.AddScoped<IRegisterUserValidator, RegisterUserValidator>();

        // Factories
        services.AddScoped<IUserFactory, UserFactory>();
        services.AddScoped<ICarFactory, CarFactory>();
        services.AddScoped<ICarPhotoFactory, CarPhotoFactory>();
        services.AddScoped<IContactFactory, ContactFactory>();
        services.AddScoped<IListingFactory, ListingFactory>();

        // Helpers
        services.AddScoped<IMoneyFactory, MoneyFactory>();
        services.AddScoped<ICarSearcher, CarSearcher>();
        services.AddScoped<ICarSellerGuard, CarSellerGuard>();
        services.AddScoped<IUserSearcher, UserSearcher>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordResetTokenGenerator, PasswordResetTokenGenerator>();
        services.AddScoped<IContactSearcher, ContactSearcher>();
        services.AddScoped<IContactSellerGuard, ContactSellerGuard>();
        services.AddScoped<ICreateContactValidator, CreateContactValidator>();
        services.AddScoped<IListingSearcher, ListingSearcher>();
        services.AddScoped<IListingSellerGuard, ListingSellerGuard>();
    }
}