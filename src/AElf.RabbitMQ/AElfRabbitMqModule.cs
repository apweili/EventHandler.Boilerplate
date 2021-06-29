using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace AElf.RabbitMQ
{
    [DependsOn(
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpBackgroundJobsRabbitMqModule)
    )]
    public class AElfRabbitMqModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.RemoveAll(typeof(IJobQueue<>));
            context.Services.AddSingleton(typeof(IJobQueue<>), typeof(RabbitJobQueue<>));
        }
    }
}