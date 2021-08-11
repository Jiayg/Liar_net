using System;
using System.Threading.Channels;

namespace Liar.Core.Helper
{
    /// <summary>
    /// 一个专门处理数据流相关操作的类 用来在生产者和订阅者之间传递数据
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ChannelHelper<TModel>
    {
        private static readonly Lazy<ChannelHelper<TModel>> lazy = new Lazy<ChannelHelper<TModel>>(() => new ChannelHelper<TModel>());

        private readonly ChannelWriter<TModel> _writer;
        private readonly ChannelReader<TModel> _reader;
        static ChannelHelper()
        {
        }

        private ChannelHelper()
        {
            var channelOptions = new BoundedChannelOptions(1000)
            {
                FullMode = BoundedChannelFullMode.DropOldest
            };
            var channel = Channel.CreateBounded<TModel>(channelOptions);
            _writer = channel.Writer;
            _reader = channel.Reader;
        }
        public static ChannelHelper<TModel> Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public ChannelWriter<TModel> Writer => _writer;

        public ChannelReader<TModel> Reader => _reader;
    }
}
