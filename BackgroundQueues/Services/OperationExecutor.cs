using BackgroundQueues.Events;
using BackgroundQueues.Models;
using Coravel.Events.Interfaces;

namespace BackgroundQueues.Services
{
    public class OperationExecutor(IDispatcher dispatcher, ILogger<OperationExecutor> logger)
    {
        public async Task ExecuteWorkItemAsync(OperationItem workItem, CancellationToken cancellationToken)
        {
            logger.LogInformation("WorkItem \'{Name}\' was started", workItem.Name);

            await dispatcher.Broadcast(new OperationStartedEvent(workItem));
            await dispatcher.Broadcast(new OperationValidatedEvent(workItem));
            await dispatcher.Broadcast(new OperationExecutedEvent(workItem));
            await dispatcher.Broadcast(new OperationFinishedEvent(workItem));

            logger.LogInformation("WorkItem \'{Name}\' was finished", workItem.Name);
        }
    }
}
