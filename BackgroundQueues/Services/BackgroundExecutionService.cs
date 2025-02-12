using System.Runtime.CompilerServices;
using BackgroundQueues.Models;

namespace BackgroundQueues.Services
{
    public class BackgroundExecutionService(
        OperationQueue operationQueue, 
        IServiceProvider serviceProvider,
        ILogger<BackgroundExecutionService> logger) 
        : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var executor = scope.ServiceProvider.GetRequiredService<OperationExecutor>();

            await Parallel.ForEachAsync(ReadFromQueueAsync(stoppingToken), new ParallelOptions
            {
                MaxDegreeOfParallelism = Environment.ProcessorCount,
                CancellationToken = stoppingToken
            }, async (workItem, ct) =>
            {
                try
                {
                    logger.LogInformation(@"Background service is starting execution of workItem {Name}", workItem.Name);
                    await executor.ExecuteWorkItemAsync(workItem, ct);
                    logger.LogInformation(@"Background service finished execution of workItem {Name}", workItem.Name);
                }
                catch (OperationCanceledException)
                {
                    logger.LogWarning(@"Execution of workItem was canceled");
                }
            });
        }

        private async IAsyncEnumerable<OperationItem> ReadFromQueueAsync([EnumeratorCancellation] CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("Background service is waiting for workItem from the queue...");
                var workItem = await operationQueue.DequeueWorkItemAsync(stoppingToken);
                logger.LogInformation(@"Background service was received workItem {Name} from the queue", workItem.Name);
                yield return workItem;
            }
        }
    }
}
