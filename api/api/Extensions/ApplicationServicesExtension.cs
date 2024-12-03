using api.Interfaces;
using api.Repositories;
using api.Services;

namespace api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            string TOKENKEY = config["TokenKey"] ?? "";
            string connString = config.GetConnectionString("MySQL") ?? "";

            // Accounts Service
            services.AddScoped<IAccountsRepo, AccountsRepo>(provider =>
            {
                return new AccountsRepo(connString);
            });

            // Token Service
            services.AddScoped<ITokenService, TokenService>(provider =>
            {
                return new TokenService(TOKENKEY);
            });

            // Users Service
            services.AddScoped<IUsersRepo, UsersRepo>(provider =>
            {
                return new UsersRepo(connString);
            });

            return services;
        }
    }
}
