using System.Collections.Concurrent;
using System.Threading.Channels;
using BackgroundQueues.Models;

namespace BackgroundQueues.Services
{
    public class OperationQueue
    {
        private readonly Channel<OperationItem> _queue;
        private readonly ILogger<OperationQueue> _logger;

        public OperationQueue(ILogger<OperationQueue> logger)
        {
            _queue = Channel.CreateUnbounded<OperationItem>();
            _logger = logger;
        }

        public async ValueTask QueueWorkItemAsync(OperationItem workItem, CancellationToken cancellationToken = default)
        {
            await _queue.Writer.WriteAsync(workItem, cancellationToken);
            _logger.LogInformation(@"WorkItem {Name} was added into the queue", workItem.Name);
        }

        public async ValueTask<OperationItem> DequeueWorkItemAsync(CancellationToken cancellationToken = default)
        {
            var workItem = await _queue.Reader.ReadAsync(cancellationToken);
            _logger.LogInformation(@"WorkItem {Name} was pulled from the queue", workItem.Name);
            return workItem;
        }
    }
}
