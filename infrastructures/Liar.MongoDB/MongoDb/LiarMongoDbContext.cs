using Liar.MongoDB.MongoEntities;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Liar.MongoDB.MongoDb
{
    [ConnectionStringName("MongoDbConnectionString")]
    public class LiarMongoDbContext : AbpMongoDbContext
    {
        public IMongoCollection<LoginLog> LoginLog => Collection<LoginLog>();
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.Entity<LoginLog>(b =>
            {
                b.CollectionName = "LoginLog";
            });
        }
    }
}
