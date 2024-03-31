﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherApplication.Models;

#nullable disable

namespace WeatherApplication.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240331100823_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WeatherApplication.Models.Weather", b =>
                {
                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Cloudy")
                        .HasColumnType("int");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Humidity")
                        .HasColumnType("float");

                    b.Property<string>("Phenomenon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pressure")
                        .HasColumnType("int");

                    b.Property<int>("T")
                        .HasColumnType("int");

                    b.Property<double>("Td")
                        .HasColumnType("float");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VV")
                        .HasColumnType("int");

                    b.Property<int>("Velocity")
                        .HasColumnType("int");

                    b.Property<int>("h")
                        .HasColumnType("int");

                    b.HasKey("Data");

                    b.ToTable("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}