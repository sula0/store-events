using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain;
using Store.Core.Domain.Event;
using Store.Core.Domain.Projection;
using Store.Order.Infrastructure;
using Store.Order.Infrastructure.Entity;

namespace Store.Order.Application.Buyer.Projections.Cart
{
    public class CartProjectionManager : IProjectionManager
    {
        private const string SubscriptionId = nameof(CartEntity);
        
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IProjection<CartEntity, StoreOrderDbContext> _projection;
        private readonly IEventSubscriptionFactory _eventSubscriptionFactory;
        private readonly ICheckpointRepository _checkpointRepository;

        public CartProjectionManager(
            IServiceScopeFactory scopeFactory,
            IProjection<CartEntity, StoreOrderDbContext> projection,
            IEventSubscriptionFactory eventSubscriptionFactory,
            ICheckpointRepository checkpointRepository)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            _projection = projection ?? throw new ArgumentNullException(nameof(projection));
            _eventSubscriptionFactory = eventSubscriptionFactory ?? throw new ArgumentNullException(nameof(eventSubscriptionFactory));
            _checkpointRepository = checkpointRepository ?? throw new ArgumentNullException(nameof(checkpointRepository));
        }
        
        public async Task StartAsync()
        {
            ulong checkpoint = await _checkpointRepository.GetAsync(SubscriptionId);
            await _eventSubscriptionFactory
                .Create(SubscriptionId, ProjectEventAsync)
                .SubscribeAtAsync(checkpoint);
        }

        public Task StopAsync() => Task.CompletedTask;
        
        private async Task ProjectEventAsync(IEvent @event, EventMetadata eventMetadata)
        {
            Ensure.NotNull(@event, nameof(@event));

            using IServiceScope scope = _scopeFactory.CreateScope();

            StoreOrderDbContext context = scope.ServiceProvider.GetRequiredService<StoreOrderDbContext>();
            if (context == null) return;

            Func<Task> projectionAction = _projection.Project(@event, context);
            if (projectionAction == null) return;

            // TODO: think about ref model parameter
            await projectionAction();
            // TODO: should be in the same unit of work as the projection changes.
            await _checkpointRepository.SaveAsync(SubscriptionId, eventMetadata.StreamPosition);
            
            await context.SaveChangesAsync();
        }
    }
}