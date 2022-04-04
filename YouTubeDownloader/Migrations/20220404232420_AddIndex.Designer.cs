﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YouTubeDownloader.Contexts;

#nullable disable

namespace YouTubeDownloader.Migrations
{
    [DbContext(typeof(SongsDbContext))]
    [Migration("20220404232420_AddIndex")]
    partial class AddIndex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("YouTubeDownloader.Models.Song", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<string>("Album")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Artist")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<int>("Downloaded")
                        .HasColumnType("INTEGER");
                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasAnnotation("Sqlite:Autoincrement", true);
                    b.HasKey("Id");

                    b.ToTable("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
