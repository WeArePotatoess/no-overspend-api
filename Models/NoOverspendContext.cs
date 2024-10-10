using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace No_Overspend_Api.Models
{
    public class NoOverspendContext : DbContext
    {
        public NoOverspendContext() { }
        public NoOverspendContext(DbContextOptions<NoOverspendContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
