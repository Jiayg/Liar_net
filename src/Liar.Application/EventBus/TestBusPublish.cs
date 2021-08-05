using System.Threading.Tasks;
using System;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace Liar.Application.EventBus
{
    public class TestBusPublish : ITransientDependency
    {
        private readonly IDistributedEventBus _distributedEventBus;

        public TestBusPublish(IDistributedEventBus distributedEventBus)
        {
            this._distributedEventBus = distributedEventBus;
        }

        /// <summary>
        /// 发布订阅
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newCount"></param>
        /// <returns></returns>
        public virtual async Task ChangeStockCountAsync(string productId, int newCount)
        {
            await _distributedEventBus.PublishAsync(
                new StockCountChangedEto
                {
                    ProductId = productId,
                    NewCount = newCount
                }
            );
        }
    }
}
