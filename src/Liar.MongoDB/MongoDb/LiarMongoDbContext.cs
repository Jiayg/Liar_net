using Volo.Abp.MongoDB;

namespace Liar.MongoDB.MongoDb
{
    public class LiarMongoDbContext : AbpMongoDbContext
    {
        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder); 
        }
    }
}
