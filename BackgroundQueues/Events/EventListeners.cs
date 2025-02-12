using Coravel.Events.Interfaces;

namespace BackgroundQueues.Events
{
    public class OperationStartedEventListener(ILogger<OperationStartedEventListener> logger) : 
        IListener<OperationStartedEvent>
    {
        public async Task HandleAsync(OperationStartedEvent broadcasted)
        {
            logger.LogInformation(@"Operation {Name} is starting initialize", broadcasted.WorkItem.Name);
            await Task.Delay(TimeSpan.FromSeconds(3));
            logger.LogInformation(@"Operation {Name} was initialized", broadcasted.WorkItem.Name);
        }
    }

    public class OperationValidatedEventListener(ILogger<OperationValidatedEventListener> logger) :
        IListener<OperationValidatedEvent>
    {
        public async Task HandleAsync(OperationValidatedEvent broadcasted)
        {
            logger.LogInformation(@"Operation {Name} is starting validate", broadcasted.WorkItem.Name);
            await Task.Delay(TimeSpan.FromSeconds(3));
            logger.LogInformation(@"Operation {Name} was validated", broadcasted.WorkItem.Name);
        }
    }

    public class OperationExecutedEventListener(ILogger<OperationExecutedEventListener> logger) :
        IListener<OperationExecutedEvent>
    {
        public async Task HandleAsync(OperationExecutedEvent broadcasted)
        {
            logger.LogInformation(@"Operation {Name} is starting execute", broadcasted.WorkItem.Name);
            await Task.Delay(TimeSpan.FromSeconds(7));
            logger.LogInformation(@"Operation {Name} was executed", broadcasted.WorkItem.Name);
        }
    }

    public class OperationFinishedEventListener(ILogger<OperationFinishedEventListener> logger) :
        IListener<OperationFinishedEvent>
    {
        public async Task HandleAsync(OperationFinishedEvent broadcasted)
        {
            logger.LogInformation(@"Operation {Name} is starting finish", broadcasted.WorkItem.Name);
            await Task.Delay(TimeSpan.FromSeconds(1));
            logger.LogInformation(@"Operation {Name} was finished", broadcasted.WorkItem.Name);
        }
    }
}
