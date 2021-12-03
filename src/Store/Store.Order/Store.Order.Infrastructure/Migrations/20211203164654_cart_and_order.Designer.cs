﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Store.Order.Infrastructure;

#nullable disable

namespace Store.Order.Infrastructure.Migrations
{
    [DbContext(typeof(StoreOrderDbContext))]
    [Migration("20211203164654_cart_and_order")]
    partial class cart_and_order
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Store.Core.Infrastructure.EntityFramework.Entity.SubscriptionCheckpointEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("Position")
                        .HasColumnType("numeric(20,0)")
                        .HasColumnName("position");

                    b.Property<string>("SubscriptionId")
                        .HasColumnType("text")
                        .HasColumnName("subscription_id");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("subscription_checkpoint", "public");
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.CartEntryEntity", b =>
                {
                    b.Property<string>("CustomerNumber")
                        .HasColumnType("text")
                        .HasColumnName("customer_number");

                    b.Property<string>("SessionId")
                        .HasColumnType("text")
                        .HasColumnName("session_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("ProductCatalogueNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("product_catalogue_number");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint")
                        .HasColumnName("quantity");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("CustomerNumber", "SessionId");

                    b.HasIndex("ProductCatalogueNumber");

                    b.ToTable("cart_entry", "public");
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.OrderEntity", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("CustomerNumber")
                        .HasColumnType("text")
                        .HasColumnName("customer_number");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("total_amount");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("OrderId");

                    b.ToTable("order", "public");
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.OrderLineEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("Count")
                        .HasColumnType("bigint")
                        .HasColumnName("count");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<string>("ProductCatalogueNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("product_catalogue_number");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("total_amount");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductCatalogueNumber");

                    b.ToTable("order_line", "public");
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.ProductEntity", b =>
                {
                    b.Property<string>("CatalogueNumber")
                        .HasColumnType("text")
                        .HasColumnName("catalogue_number");

                    b.Property<bool>("Available")
                        .HasColumnType("boolean")
                        .HasColumnName("available");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("CatalogueNumber");

                    b.ToTable("product", "public");
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.ShippingInformationEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<int>("CountryCode")
                        .HasColumnType("integer")
                        .HasColumnName("country_code");

                    b.Property<string>("FullName")
                        .HasColumnType("text")
                        .HasColumnName("full_name");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<string>("Postcode")
                        .HasColumnType("text")
                        .HasColumnName("postcode");

                    b.Property<string>("StateProvince")
                        .HasColumnType("text")
                        .HasColumnName("state_province");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("text")
                        .HasColumnName("street_address");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("shipping_information", "public");
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.CartEntryEntity", b =>
                {
                    b.HasOne("Store.Order.Infrastructure.Entity.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductCatalogueNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.OrderLineEntity", b =>
                {
                    b.HasOne("Store.Order.Infrastructure.Entity.OrderEntity", "OrderEntity")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Store.Order.Infrastructure.Entity.ProductEntity", "Product")
                        .WithMany()
                        .HasForeignKey("ProductCatalogueNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderEntity");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.ShippingInformationEntity", b =>
                {
                    b.HasOne("Store.Order.Infrastructure.Entity.OrderEntity", null)
                        .WithOne("ShippingInformation")
                        .HasForeignKey("Store.Order.Infrastructure.Entity.ShippingInformationEntity", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Store.Order.Infrastructure.Entity.OrderEntity", b =>
                {
                    b.Navigation("OrderLines");

                    b.Navigation("ShippingInformation");
                });
#pragma warning restore 612, 618
        }
    }
}
