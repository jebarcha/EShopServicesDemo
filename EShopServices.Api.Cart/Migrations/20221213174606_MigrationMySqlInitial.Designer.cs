﻿// <auto-generated />
using System;
using EShopServices.Api.Cart.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EShopServices.Api.Cart.Migrations
{
    [DbContext(typeof(CartContext))]
    [Migration("20221213174606_MigrationMySqlInitial")]
    partial class MigrationMySqlInitial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EShopServices.Api.Cart.Model.CartSession", b =>
                {
                    b.Property<int>("CartSessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("CartSessionId");

                    b.ToTable("CartSession");
                });

            modelBuilder.Entity("EShopServices.Api.Cart.Model.CartSessionDetail", b =>
                {
                    b.Property<int>("CartSessionDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CartSessionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SelectedProduct")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CartSessionDetailId");

                    b.HasIndex("CartSessionId");

                    b.ToTable("CartSessionDetail");
                });

            modelBuilder.Entity("EShopServices.Api.Cart.Model.CartSessionDetail", b =>
                {
                    b.HasOne("EShopServices.Api.Cart.Model.CartSession", "CartSession")
                        .WithMany("DetailList")
                        .HasForeignKey("CartSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartSession");
                });

            modelBuilder.Entity("EShopServices.Api.Cart.Model.CartSession", b =>
                {
                    b.Navigation("DetailList");
                });
#pragma warning restore 612, 618
        }
    }
}
