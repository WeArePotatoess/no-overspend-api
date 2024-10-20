using No_Overspend_Api.Services;

namespace No_Overspend_Api
{
    public static class ServiceInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAnalysisService, AnalysisService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IBudgetService, BudgetService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<ISavingService, SavingService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
        }
    }
}
