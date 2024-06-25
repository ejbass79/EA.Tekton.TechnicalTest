using MediatR;

namespace EA.Tekton.TechnicalTest.Cross.Abstractions.Messaging;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
 
}
