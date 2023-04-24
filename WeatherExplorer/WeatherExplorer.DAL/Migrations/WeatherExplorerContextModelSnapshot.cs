﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WeatherExplorer.DAL;

#nullable disable

namespace WeatherExplorer.DAL.Migrations
{
    [DbContext(typeof(WeatherExplorerContext))]
    partial class WeatherExplorerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WeatherExplorer.DAL.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WeatherExplorer.DAL.Models.Weather", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid");

                    b.Property<byte?>("Cloudy")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<float>("DewPoint")
                        .HasColumnType("real");

                    b.Property<byte?>("HorizontalVisibility")
                        .HasColumnType("smallint");

                    b.Property<byte>("Humidity")
                        .HasColumnType("smallint");

                    b.Property<short?>("LowerborderClouds")
                        .HasColumnType("smallint");

                    b.Property<int>("Pressure")
                        .HasColumnType("integer");

                    b.Property<float>("Temprature")
                        .HasColumnType("real");

                    b.Property<TimeOnly>("Time")
                        .HasColumnType("time without time zone");

                    b.Property<Guid>("WeatherConditionId")
                        .HasColumnType("uuid");

                    b.Property<string>("WindDirection")
                        .HasColumnType("text");

                    b.Property<byte?>("WindSpeed")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("WeatherConditionId");

                    b.HasIndex("CityId", "Date", "Time")
                        .IsUnique();

                    b.ToTable("Weathers");
                });

            modelBuilder.Entity("WeatherExplorer.DAL.Models.WeatherCondition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("WeatherConditions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("05a05af8-6871-4ca5-aa16-934c18a95222"),
                            CreatedOn = new DateTime(2023, 4, 24, 17, 3, 24, 173, DateTimeKind.Utc).AddTicks(6463),
                            Name = "штиль"
                        });
                });

            modelBuilder.Entity("WeatherExplorer.DAL.Models.Weather", b =>
                {
                    b.HasOne("WeatherExplorer.DAL.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("WeatherExplorer.DAL.Models.WeatherCondition", "WeatherCondition")
                        .WithMany()
                        .HasForeignKey("WeatherConditionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("WeatherCondition");
                });
#pragma warning restore 612, 618
        }
    }
}
