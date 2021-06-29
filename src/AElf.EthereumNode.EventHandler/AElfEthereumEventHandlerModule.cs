using AElf.EthereumNode.EventHandler.Configuration;
using AElf.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Modularity;

namespace AElf.EthereumNode.EventHandler
{
    [DependsOn(
        typeof(AElfRabbitMqModule))]
    public class AElfEthereumEventHandlerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpRabbitMqBackgroundJobOptions>(options =>
            {
                options.JobQueues[typeof(EthereumEventParallelHandler)] =
                    new JobQueueConfiguration(typeof(EthereumEventParallelHandler), "");
            });
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            var option = context.ServiceProvider.GetRequiredService<IOptions<EthereumBackgroundJobOption>>().Value;
            var abpRabbitMqBackgroundJobOptions = context.ServiceProvider
                .GetRequiredService<IOptions<AbpRabbitMqBackgroundJobOptions>>().Value;
            var backgroundOption = new JobQueueConfiguration(
                typeof(EthereumEventParallelHandler), option.QueueName,
                option.ConnectionName,true,false,true);
            backgroundOption.Arguments["WorkerCount"] = option.ParallelWorker;
            abpRabbitMqBackgroundJobOptions.JobQueues[typeof(EthereumEventParallelHandler)] =
                backgroundOption;
        }
    }
}