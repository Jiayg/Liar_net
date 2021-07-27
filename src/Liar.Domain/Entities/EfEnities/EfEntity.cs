using Volo.Abp.Domain.Entities;

namespace Liar.Domain.Entities
{
    public abstract class EfEntity : Entity, IEntity<long>
    {
    }
}
