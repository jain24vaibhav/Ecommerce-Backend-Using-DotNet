﻿// <auto-generated />
using System;
using Ecommerce.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecommerce.Migrations
{
    [DbContext(typeof(EcommerceContext))]
    [Migration("20191010102721_Order tables added")]
    partial class Ordertablesadded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Ecommerce.models.Address", b =>
                {
                    b.Property<int>("addressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("area");

                    b.Property<string>("city");

                    b.Property<int>("houseno");

                    b.Property<string>("name");

                    b.Property<int>("orderId");

                    b.Property<string>("pincode");

                    b.Property<string>("state");

                    b.HasKey("addressId");

                    b.HasIndex("orderId");

                    b.ToTable("addresse");
                });

            modelBuilder.Entity("Ecommerce.models.Cart", b =>
                {
                    b.Property<int>("productId");

                    b.Property<int>("userId");

                    b.HasKey("productId", "userId");

                    b.HasIndex("userId");

                    b.ToTable("cart");
                });

            modelBuilder.Entity("Ecommerce.models.Department", b =>
                {
                    b.Property<int>("departmentId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("departmentImage");

                    b.Property<string>("departmentName");

                    b.HasKey("departmentId");

                    b.ToTable("departments");
                });

            modelBuilder.Entity("Ecommerce.models.order", b =>
                {
                    b.Property<int>("orderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("orderDate");

                    b.Property<int>("userId");

                    b.HasKey("orderId");

                    b.ToTable("order");
                });

            modelBuilder.Entity("Ecommerce.models.OrderDetail", b =>
                {
                    b.Property<int>("orderIdDetail")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("orderId");

                    b.Property<int>("productId");

                    b.HasKey("orderIdDetail");

                    b.HasIndex("orderId");

                    b.ToTable("orderDetail");
                });

            modelBuilder.Entity("Ecommerce.models.Product", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("creationDate");

                    b.Property<int>("departmentId");

                    b.Property<string>("productDescription");

                    b.Property<string>("productImage");

                    b.Property<int>("productMRP");

                    b.Property<string>("productName");

                    b.Property<int>("productPrice");

                    b.Property<int>("quantityAvailable");

                    b.Property<int>("quantitySold");

                    b.Property<DateTime>("updationDate");

                    b.HasKey("productId");

                    b.HasIndex("departmentId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Ecommerce.models.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("userEmail");

                    b.Property<string>("userFirstName");

                    b.Property<string>("userLastName");

                    b.Property<string>("userMobile");

                    b.Property<string>("userPassword");

                    b.Property<string>("userRole");

                    b.HasKey("userId");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Ecommerce.models.Address", b =>
                {
                    b.HasOne("Ecommerce.models.order", "Order")
                        .WithMany()
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ecommerce.models.Cart", b =>
                {
                    b.HasOne("Ecommerce.models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Ecommerce.models.User", "User")
                        .WithMany()
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ecommerce.models.OrderDetail", b =>
                {
                    b.HasOne("Ecommerce.models.order", "Order")
                        .WithMany()
                        .HasForeignKey("orderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Ecommerce.models.Product", b =>
                {
                    b.HasOne("Ecommerce.models.Department", "Departments")
                        .WithMany()
                        .HasForeignKey("departmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
