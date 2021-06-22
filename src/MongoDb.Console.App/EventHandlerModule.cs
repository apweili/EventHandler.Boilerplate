using System;
using System.Net.Security;
using System.Security.Authentication;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDb.Console.MongoDB;
using RabbitMQ.Client;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;
using Volo.Abp.RabbitMQ;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Hangfire;
using StackExchange.Redis;

namespace MongoDb.Console.App
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(MongoDbModule),
        typeof(AbpEventBusRabbitMqModule),
        typeof(AbpBackgroundJobsHangfireModule)
    )]
    public class EventHandlerModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            ConfigureEventBusOption(configuration);
            ConfigureRabbitMqOption(configuration);
            ConfigureBackgroundJobOption(configuration);
            
            //var redis = ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis"));
            context.Services.AddHangfire(config =>
            {
                config.UseInMemoryStorage();
                //config.UseRedisStorage(redis);
            });

            context.Services.AddHostedService<AppHostedService>();
        }

        private void ConfigureEventBusOption(IConfiguration configuration)
        {
            Configure<AbpRabbitMqEventBusOptions>(options =>
            {
                var messageQueueConfig = configuration.GetSection("MessageQueue");
                options.ClientName = messageQueueConfig.GetSection("ClientName").Value;
                options.ExchangeName = messageQueueConfig.GetSection("ExchangeName").Value;
            });
        }
        
        private void ConfigureRabbitMqOption(IConfiguration configuration)
        {
            Configure<AbpRabbitMqOptions>(options =>
            {
                var messageQueueConfig = configuration.GetSection("MessageQueue");
                var hostName = messageQueueConfig.GetSection("HostName").Value;

                options.Connections.Default.HostName = hostName;
                options.Connections.Default.Port = int.Parse(messageQueueConfig.GetSection("Port").Value);
                options.Connections.Default.UserName = messageQueueConfig.GetSection("UserName").Value;
                options.Connections.Default.Password = messageQueueConfig.GetSection("Password").Value;
                options.Connections.Default.Ssl = new SslOption
                {
                    Enabled = true,
                    ServerName = hostName,
                    Version = SslProtocols.Tls12,
                    AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch |
                                             SslPolicyErrors.RemoteCertificateChainErrors
                };
                options.Connections.Default.VirtualHost = "/";
                options.Connections.Default.Uri = new Uri(messageQueueConfig.GetSection("Uri").Value);
            });
        }

        private void ConfigureBackgroundJobOption(IConfiguration configuration)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = true;
            });
        }
        
        private void ConfigureHangfireServer(IServiceCollection services, IConfiguration configuration)
        {
            var queueName = new[] {"serial"};
            services.AddHangfireServer(options =>
            {
                options.Queues = queueName;
                options.WorkerCount = 1;
            });
        }
    }
}
