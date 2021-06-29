using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.RabbitMQ;

namespace AElf.RabbitMQ
{
    public class RabbitJobQueue<TArgs> : JobQueue<TArgs>
    {
        private const string ChannelPrefix = "JobQueue.";
        private const string WorkerCount = "WorkCount";
        public RabbitJobQueue(IOptions<AbpBackgroundJobOptions> backgroundJobOptions,
            IOptions<AbpRabbitMqBackgroundJobOptions> rabbitMqAbpBackgroundJobOptions, IChannelPool channelPool,
            IRabbitMqSerializer serializer, IBackgroundJobExecuter jobExecuter,
            IServiceScopeFactory serviceScopeFactory, IExceptionNotifier exceptionNotifier) : base(backgroundJobOptions,
            rabbitMqAbpBackgroundJobOptions, channelPool, serializer, jobExecuter, serviceScopeFactory,
            exceptionNotifier)
        {
        }
        protected List<IChannelAccessor> ChannelAccessors { get; private set; }
        protected new bool IsDiposed { get; private set; }

        protected override Task EnsureInitializedAsync()
        {
            if (ChannelAccessors != null)
            {
                return Task.CompletedTask;
            }

            ChannelAccessors = new List<IChannelAccessor>();
            if (!QueueConfiguration.Arguments.TryGetValue(WorkerCount, out var workerCount))
            {
                return Task.CompletedTask;
            }
            ChannelAccessors = Enumerable.Range(1, (int) workerCount).Select(x => GenerateChannelAccessor()).ToList();
            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            if (IsDiposed)
            {
                return;
            }

            IsDiposed = true;
            ChannelAccessors?.ForEach(x => x.Dispose());
        }
        
        private IChannelAccessor GenerateChannelAccessor()
        {
            var channelAccessor = ChannelPool.Acquire(
                ChannelPrefix + QueueConfiguration.QueueName,
                QueueConfiguration.ConnectionName
            );
            
            var result = QueueConfiguration.Declare(channelAccessor.Channel);
            Logger.LogDebug($"RabbitMQ Queue '{QueueConfiguration.QueueName}' has {result.MessageCount} messages and {result.ConsumerCount} consumers.");
            
            if (AbpBackgroundJobOptions.IsJobExecutionEnabled)
            {
                var consumer = new AsyncEventingBasicConsumer(channelAccessor.Channel);
                consumer.Received += MessageReceived;
            
                //TODO: What BasicConsume returns?
                channelAccessor.Channel.BasicConsume(
                    queue: QueueConfiguration.QueueName,
                    autoAck: QueueConfiguration.AutoDelete,
                    consumer: consumer
                );
            }

            return channelAccessor;
        }
        
        protected override async Task MessageReceived(object sender, BasicDeliverEventArgs ea)
        {
            using var scope = ServiceScopeFactory.CreateScope();
            var context = new JobExecutionContext(
                scope.ServiceProvider,
                JobConfiguration.JobType,
                Serializer.Deserialize(ea.Body.ToArray(), typeof(TArgs))
            );
                
            try
            {
                await JobExecuter.ExecuteAsync(context);
                if (!QueueConfiguration.AutoDelete)
                {
                    ChannelAccessor.Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                    
            }
            catch (BackgroundJobExecutionException)
            {
                if (!QueueConfiguration.AutoDelete)
                {
                    ChannelAccessor.Channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: true);
                }
            }
            catch (Exception)
            {
                if (!QueueConfiguration.AutoDelete)
                {
                    ChannelAccessor.Channel.BasicReject(deliveryTag: ea.DeliveryTag, requeue: false);
                }
            }
        }
    }
}