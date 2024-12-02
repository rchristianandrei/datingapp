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

            services.AddScoped<IAccountsRepo, AccountsRepo>(provider =>
            {
                return new AccountsRepo(config.GetConnectionString("MySQL") ?? "");
            });

            services.AddScoped<ITokenService, TokenService>(provider =>
            {
                return new TokenService(TOKENKEY);
            });
            return services;
        }
    }
}
