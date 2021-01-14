﻿// <auto-generated />
using System;
using CostJanitor.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CostJanitor.Infrastructure.Migrations
{
    [DbContext(typeof(DomainContext))]
    partial class DomainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CostJanitor.Domain.Aggregates.CostItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CostItem");
                });

            modelBuilder.Entity("CostJanitor.Domain.Aggregates.CostItemReference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Added")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("ReportItemId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ReportItemId");

                    b.ToTable("CostItemReferences");
                });

            modelBuilder.Entity("CostJanitor.Domain.Aggregates.ReportItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("ReportItem");
                });

            modelBuilder.Entity("CostJanitor.Domain.Aggregates.CostItemReference", b =>
                {
                    b.HasOne("CostJanitor.Domain.Aggregates.ReportItem", null)
                        .WithMany("CostItemReferences")
                        .HasForeignKey("ReportItemId");
                });

            modelBuilder.Entity("CostJanitor.Domain.Aggregates.ReportItem", b =>
                {
                    b.Navigation("CostItemReferences");
                });
#pragma warning restore 612, 618
        }
    }
}
