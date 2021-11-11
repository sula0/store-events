using System;
using Store.Core.Domain.Event;

namespace Store.Order.Domain.Orders.Events
{
    public record OrderCreatedEvent(Guid EntityId, string CustomerNumber) : IEvent;
}