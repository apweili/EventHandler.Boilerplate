using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDb.Console.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Modularity;

namespace MongoDb.Console.App
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(MongoDbModule),
        typeof(AbpBackgroundJobsRabbitMqModule)
    )]
    public class AppModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<AppHostedService>();
        }
    }
}
