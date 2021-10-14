﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Store.Core.Domain
{
    public abstract class AggregateEntity
    {
        private readonly Dictionary<Type, Action<IEvent>> _eventAppliers;

        protected readonly ICollection<IEvent> _events;
        
        public Guid Id { get; }

        public int Version { get; }

        protected AggregateEntity()
        {
            _events = new List<IEvent>();
            _eventAppliers = new();
            
            RegisterAppliers();
        }
        
        public void ApplyEvent(IEvent domainEvent)
        {
            _eventAppliers[domainEvent.GetType()](domainEvent);
            _events.Add(domainEvent);
        }

        public IReadOnlyCollection<IEvent> GetUncommittedEvents() => _events.ToImmutableList();

        protected abstract void RegisterAppliers();
        
        protected void RegisterApplier<TEvent>(Action<TEvent> applier) where TEvent: IEvent
        {
            _eventAppliers.Add(typeof(TEvent), e => applier((TEvent)e));
        }
    }
}