﻿// <auto-generated />
using System;
using BGwalks.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BGwalks.API.Migrations
{
    [DbContext(typeof(BGWalksDbContext))]
    [Migration("20250223074626_imagesTableDBset")]
    partial class imagesTableDBset
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BGwalks.API.Models.Domain.DifficultyDomain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4b37fb9d-c26b-49e4-8e2a-1cf8da41ec9c"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("30d6a99c-8787-4919-8552-c145a7214a3a"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("e1709571-0df8-45c9-8a7f-98f2b194a046"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("BGwalks.API.Models.Domain.Models.ImageDomain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("ImageDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageExtention")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("BGwalks.API.Models.Domain.RegionDomain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6d9a5989-5262-4403-8998-a4075088c650"),
                            Name = "North America",
                            RegionImageUrl = "https://example.com/north-america.jpg"
                        },
                        new
                        {
                            Id = new Guid("4459f691-b307-4543-8765-1413477a6b84"),
                            Name = "South America",
                            RegionImageUrl = "https://example.com/south-america.jpg"
                        });
                });

            modelBuilder.Entity("BGwalks.API.Models.Domain.WalkDomain", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("regionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("regionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("BGwalks.API.Models.Domain.WalkDomain", b =>
                {
                    b.HasOne("BGwalks.API.Models.Domain.DifficultyDomain", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BGwalks.API.Models.Domain.RegionDomain", "Region")
                        .WithMany()
                        .HasForeignKey("regionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
