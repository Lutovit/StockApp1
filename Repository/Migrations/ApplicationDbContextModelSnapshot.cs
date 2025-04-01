﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository.Context;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Repository.Entities.Candle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double?>("Close")
                        .HasColumnType("double precision");

                    b.Property<double?>("High")
                        .HasColumnType("double precision");

                    b.Property<double?>("Low")
                        .HasColumnType("double precision");

                    b.Property<double?>("Open")
                        .HasColumnType("double precision");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.Property<double>("Volume")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Candles");
                });

            modelBuilder.Entity("Repository.Entities.PriceDifference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double?>("Close")
                        .HasColumnType("double precision");

                    b.Property<double?>("High")
                        .HasColumnType("double precision");

                    b.Property<double?>("Low")
                        .HasColumnType("double precision");

                    b.Property<double?>("Open")
                        .HasColumnType("double precision");

                    b.Property<string>("Ticker")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Time")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("PriceDifferences");
                });
#pragma warning restore 612, 618
        }
    }
}
