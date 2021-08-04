using Volo.Abp.Domain.Entities;

namespace Liar.Domain.IEntities
{
    public abstract class EfEntity : Entity, IEntity<long>
    {
    }
}
