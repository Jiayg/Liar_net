using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Liar.Application.EventBus
{
    public class TestBusSubscribers : IDistributedEventHandler<StockCountChangedEto>, ITransientDependency
    {
        public async Task HandleEventAsync(StockCountChangedEto eventData)
        {
            var count = eventData.NewCount + 1;

            await Task.FromResult(count);
        }
    }
}
