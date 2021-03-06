using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Shopping.Infrastructure.Entity;

namespace Store.Shopping.Infrastructure.Configuration;

public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("order");
            
        builder.HasKey(o => o.OrderId);
        builder.Property(o => o.OrderId).HasColumnName("order_id");
            
        builder.Property(o => o.CreatedAt).HasColumnName("created_at");
        builder.Property(o => o.UpdatedAt).HasColumnName("updated_at");

        builder.Property(o => o.CustomerNumber).HasColumnName("customer_number");
        builder.Property(o => o.Data).HasColumnName("data").HasColumnType("jsonb");
    }
}