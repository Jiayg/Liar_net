using System;
using Volo.Abp.EventBus;

namespace Liar.Application.EventBus
{
    [EventName("Liar.Test.StockChange")]
    public class StockCountChangedEto
    {
        public string ProductId { get; set; }

        public int NewCount { get; set; }
    }
}
