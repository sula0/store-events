using System.Text.Json.Serialization;
using EventStore.Client;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Store.Catalogue.Application.Product.Command.Create;
using Store.Catalogue.Application.Product.Projections;
using Store.Catalogue.Domain.Product;
using Store.Catalogue.Infrastructure;
using Store.Catalogue.Integration;
using Store.Core.Domain;
using Store.Core.Domain.Event;
using Store.Core.Infrastructure;
using Store.Core.Infrastructure.EventStore;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Store.Api.CatalogueManagement", Version = "v1" }); });

builder.Services.AddMediatR(typeof(ProductCreateCommand));
    
builder.Services.AddSingleton(_ => new EventStoreClient(EventStoreClientSettings.Create(builder.Configuration["EventStore:ConnectionString"])));

builder.Services.AddScoped<IIntegrationEventMapper, CatalogueIntegrationEventMapper>();

builder.Services.AddScoped(_ => new EventStoreEventDispatcherConfiguration
{
    IntegrationStreamName = "catalogue-integration"
});
builder.Services.AddScoped<IEventDispatcher, EventStoreEventDispatcher>();
    
builder.Services.AddScoped<EventStoreAggregateRepository>();
builder.Services.AddScoped<IAggregateRepository>(provider => new IntegrationAggregateRepository(
    provider.GetRequiredService<EventStoreAggregateRepository>(),
    provider.GetRequiredService<IIntegrationEventMapper>(),
    provider.GetRequiredService<IEventDispatcher>()));
    
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddSingleton<ISerializer, JsonSerializer>();
    
builder.Services.AddDbContext<StoreCatalogueDbContext>(
    options => options.UseNpgsql(builder.Configuration["Postgres:ConnectionString"], b => b.MigrationsAssembly("Store.Catalogue.Infrastructure")));

builder.Services.AddSingleton(_ => new EventStoreConnectionConfiguration
{
    SubscriptionId = "projections"
});

builder.Services.AddSingleton<IEventSubscriptionFactory, EventStoreSubscriptionFactory>();

builder.Services.AddSingleton<IEventListener, ProductProjection>();
builder.Services.AddHostedService<EventStoreSubscriptionService>();

#endregion

#region App

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Store.Api.CatalogueManagement v1"));
}

#if DEBUG

using (IServiceScope scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = scope.ServiceProvider.GetService<StoreCatalogueDbContext>();
    context?.Database.Migrate();
}

#endif

app.UseHttpsRedirection();

app.UsePathBase(new PathString("/catalogue"));
app.UseAuthorization();

app.MapControllers();

app.Run();

#endregion