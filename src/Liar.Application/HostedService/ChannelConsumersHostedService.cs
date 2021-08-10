using System.Threading;
using System.Threading.Tasks;
using Liar.Core.Helper;
using Liar.Domain.Entities;
using Microsoft.Extensions.Hosting;

namespace Liar.Application.HostedService
{
    public class ChannelConsumersHostedService : BackgroundService
    {   
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
                        catch (System.Exception)
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
