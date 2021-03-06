using System;

namespace Store.Core.Infrastructure;

public class OutboxMessageEntity
{
    public Guid Id { get; set; }
    
    public Guid CorrelationId { get; set; }
    
    public Guid CausationId { get; set; }
    
    public DateTime CreatedAt { get; set; }
     
    public DateTime ProcessedAt { get; set; }
    
    public string Type { get; set; }
    
    public string Data { get; set; }
}