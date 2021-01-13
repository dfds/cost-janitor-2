﻿// <auto-generated />
using System;
using CostJanitor.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CostJanitor.Infrastructure.Migrations
{
    [DbContext(typeof(DomainContext))]
    [Migration("20210113130713_added_costitemreference")]
    partial class added_costitemreference
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("CostJanitor.Domain.Aggregates.CostItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CostItem");
                });

            modelBuilder.Entity("CostJanitor.Domain.Aggregates.CostItemReference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Added")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ReportItemId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReportItemId");

                    b.ToTable("CostItemReferences");
                });

            modelBuilder.Entity("CostJanitor.Domain.Aggregates.ReportItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

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
