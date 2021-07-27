using Volo.Abp.Domain.Entities;

namespace Liar.Domain.Entities
{
    public class Entity : IEntity<long>
    {
        public long Id { get; set; }

        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}
