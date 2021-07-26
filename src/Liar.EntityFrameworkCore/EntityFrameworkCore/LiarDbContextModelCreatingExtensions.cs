using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Liar.EntityFrameworkCore
{
    public static class LiarDbContextModelCreatingExtensions
    {
        public static void ConfigureLiar(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(LiarConsts.DbTablePrefix + "YourEntities", LiarConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}