using AElf.AElfNode.EventHandler.Options;
using AElf.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Modularity;

namespace AElf.AElfNode.EventHandler
{
    [DependsOn(
        typeof(AElfRabbitMqModule))]
    public class AElfNodeEventHandlerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpRabbitMqBackgroundJobOptions>(options =>
            {
                options.JobQueues[typeof(AElfEventParallelHandler)] =
                    new JobQueueConfiguration(typeof(AElfEventParallelHandler), "");
            });
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            var option = context.ServiceProvider.GetRequiredService<IOptions<AElfBackgroundJobOption>>().Value;
            var abpRabbitMqBackgroundJobOptions = context.ServiceProvider
                .GetRequiredService<IOptions<AbpRabbitMqBackgroundJobOptions>>().Value;
            var backgroundOption = new JobQueueConfiguration(
                typeof(AElfEventParallelHandler), option.QueueName,
                option.ConnectionName,true,false,true);
            backgroundOption.Arguments["WorkerCount"] = option.ParallelWorker;
            abpRabbitMqBackgroundJobOptions.JobQueues[typeof(AElfEventParallelHandler)] =
                backgroundOption;
        }
    }
}