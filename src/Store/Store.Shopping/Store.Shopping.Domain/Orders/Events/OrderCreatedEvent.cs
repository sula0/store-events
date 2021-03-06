using System;
using System.Collections.Generic;
using Store.Core.Domain.Event;

namespace Store.Shopping.Domain.Orders.Events;

public record OrderCreatedEvent(
    Guid OrderId, 
    string CustomerNumber, 
    IReadOnlyCollection<OrderLine> OrderLines,
    OrderStatus Status) : IEvent;