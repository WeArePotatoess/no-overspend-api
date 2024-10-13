using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using System.Linq.Expressions;

namespace No_Overspend_Api.Infra.Models
{
    public class NoOverspendContext : DbContext
    {
        public DbSet<account> accounts { get; set; } = null!;
        public DbSet<user> users { get; set; } = null!;
        public DbSet<app_setting> app_settings { get; set; } = null!;
        public DbSet<budget> budgets { get; set; } = null!;
        public DbSet<category> categories { get; set; } = null!;
        public DbSet<icon> icons { get; set; } = null!;
        public DbSet<notification> notifications { get; set; } = null!;
        public DbSet<saving> savings { get; set; } = null!;
        public DbSet<transaction> transactions { get; set; } = null!;
        public DbSet<role> roles { get; set; } = null!;

        public NoOverspendContext(DbContextOptions<NoOverspendContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new account_configuration());
            modelBuilder.ApplyConfiguration(new user_configuration());
            modelBuilder.ApplyConfiguration(new app_setting_configuration());
            modelBuilder.ApplyConfiguration(new budget_configuration());
            modelBuilder.ApplyConfiguration(new category_configuration());
            modelBuilder.ApplyConfiguration(new icon_configuration());
            modelBuilder.ApplyConfiguration(new notification_configuration());
            modelBuilder.ApplyConfiguration(new saving_configuration());
            modelBuilder.ApplyConfiguration(new transaction_configuration());
            modelBuilder.ApplyConfiguration(new role_configuration());


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ModelBase).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "Deleted");
                    var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);

                    entityType.SetQueryFilter(filter);
                }
            }
        }
    }
}
