using BackgroundQueues.Events;
using Coravel;

namespace BackgroundQueues.Extensions
{
    public static class EventsRegistration
    {
        public static void UseCoravelEvents(this WebApplication application)
        {
            var eventRegistration = application.Services.ConfigureEvents();

            eventRegistration
                .Register<OperationStartedEvent>()
                .Subscribe<OperationStartedEventListener>();

            eventRegistration
                .Register<OperationValidatedEvent>()
                .Subscribe<OperationValidatedEventListener>();

            eventRegistration
                .Register<OperationExecutedEvent>()
                .Subscribe<OperationExecutedEventListener>();

            eventRegistration
                .Register<OperationFinishedEvent>()
                .Subscribe<OperationFinishedEventListener>();
        }
    }
}
