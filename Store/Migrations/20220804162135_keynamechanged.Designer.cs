﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreServiceAPI.DbContexts;

#nullable disable

namespace StoreServiceAPI.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20220804162135_keynamechanged")]
    partial class keynamechanged
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StoreServiceAPI.Entities.Store", b =>
                {
                    b.Property<int>("SapNumber_id")
                        .HasColumnType("int")
                        .HasColumnName("SapNumber_id");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlowersModule")
                        .HasColumnType("int");

                    b.Property<bool>("IsFranchise")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOpenOnSunday")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SmsStoreNumber")
                        .HasColumnType("int");

                    b.HasKey("SapNumber_id");

                    b.ToTable("Stores");
                });
#pragma warning restore 612, 618
        }
    }
}
