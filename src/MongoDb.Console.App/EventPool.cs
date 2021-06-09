using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDb.Console.App.DTO;

namespace MongoDb.Console.App
{
    public class EventProvider
    {
        public ConcurrentQueue<ContractEventDetailsETO> EventEtos { get; set; }
    }

    public interface IEventPoolTaskScheduler
    {
        Task<bool> EnqueueTask(IList<ContractEventDetailsETO> etos);
    }

    public class EventPoolTaskScheduler : IEventPoolTaskScheduler
    {
        private readonly Dictionary<string, IEventProcessor> _eventProcessors;

        public EventPoolTaskScheduler(IEnumerable<IEventProcessor> eventProcessors)
        {
            _eventProcessors = eventProcessors.ToDictionary(x => x.GetEventId(), x => x);
            _currentExecutingTask = 0;
            _groupedTaskDic = new Dictionary<string, List<Task>>();
            _serialTaskDic = new Dictionary<string, Task>();
        }
        public int MaxParallelTaskCount { get; } = 10;
        private int _currentExecutingTask;

        private Dictionary<string, List<Task>> _groupedTaskDic;

        private Dictionary<string, Task> _serialTaskDic;

        public async Task<bool> EnqueueTask(IList<ContractEventDetailsETO> etos)
        {
            var currentExecutingTask = _currentExecutingTask;
            if (currentExecutingTask >= MaxParallelTaskCount)
                return false;
            if (currentExecutingTask + etos.Count > MaxParallelTaskCount)
                return false;
            var toGroupedTask = new List<ContractEventDetailsETO>();
            foreach (var eto in etos)
            {
                if (!_eventProcessors.TryGetValue(eto.GetId(), out var processor))
                {
                    //To Do, when processor does not exist
                    continue;
                }

                if (string.IsNullOrEmpty(processor.GetParallelKey(eto)))
                {
                    if (_serialTaskDic.ContainsKey(eto.GetId()))
                    {
                        _serialTaskDic[eto.GetId()] = _serialTaskDic[eto.GetId()].ContinueWith(async x =>
                        {
                            await processor.HandleEventAsync(eto);
                            Interlocked.Add(ref _currentExecutingTask, 1);
                        });
                        continue;
                    }
                    _serialTaskDic[eto.GetId()] = Task.Run(async () =>
                    {
                        await processor.HandleEventAsync(eto);
                        Interlocked.Add(ref _currentExecutingTask, 1);
                    });
                    continue;
                }
                toGroupedTask.Add(eto);
            }
            
            return true;
        }
    }
}