using Liar.Domain.MongoEntities;
using MongoDB.Driver;
using Volo.Abp.MongoDB;

namespace Liar.MongoDB.MongoDb
{
    public class LiarMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<LoginLog> Questions => Collection<LoginLog>();
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginLog>(b =>
            {
                b.CollectionName = "LoginLog";
            });
        }
    }
}
