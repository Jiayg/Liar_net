using System.Threading;
using System.Threading.Tasks;
using Liar.Core.Helper;
using Liar.MongoDB.IRepository;
using Liar.MongoDB.MongoEntities;
using Microsoft.Extensions.Hosting;

namespace Liar.Application.HostedService
{
    public class ChannelConsumersHostedService : BackgroundService
    {
        private readonly ILoginLogRepository _logMongoRepository;

        public ChannelConsumersHostedService(ILoginLogRepository logMongoRepository)
        {
            this._logMongoRepository = logMongoRepository;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //save loginlogs
            _ = Task.Run(async () =>
            {
                var channelLoginReader = ChannelHelper<LoginLog>.Instance.Reader;
                while (await channelLoginReader.WaitToReadAsync(stoppingToken))
                {
                    if (channelLoginReader.TryRead(out var entity))
                    {
                        try
                        {
                            //await _logMongoRepository.AddAsync(entity);
                        }
                        catch (System.Exception ex)
                        { 
                        }
                    }
                    if (stoppingToken.IsCancellationRequested) break;
                }
            }, stoppingToken);

            await Task.CompletedTask;
        }
    }
}
