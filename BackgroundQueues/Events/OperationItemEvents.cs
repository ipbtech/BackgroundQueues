using BackgroundQueues.Models;
using Coravel.Events.Interfaces;

namespace BackgroundQueues.Events
{
    public record OperationStartedEvent(OperationItem WorkItem) : IEvent;
    public record OperationValidatedEvent(OperationItem WorkItem) : IEvent;
    public record OperationExecutedEvent(OperationItem WorkItem) : IEvent;
    public record OperationFinishedEvent(OperationItem WorkItem) : IEvent;
}
