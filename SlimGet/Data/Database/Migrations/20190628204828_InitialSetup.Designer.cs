﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SlimGet.Services;

namespace SlimGet.Data.Database.Migrations
{
    [DbContext(typeof(SlimGetContext))]
    [Migration("20190628204828_InitialSetup")]
    partial class InitialSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SlimGet.Data.Database.Package", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<long>("DownloadCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0L);

                    b.Property<bool>("HasReadme");

                    b.Property<string>("IconUrl")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<bool>("IsListed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsPrerelase")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Language")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<string>("LicenseUrl")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<string>("MinimumClientVersion")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<string>("ProjectUrl")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<DateTime>("PublishedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("RepositoryType")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<string>("RepositoryUrl")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<bool>("RequireLicenseAcceptance")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<int>("SemVerLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("Summary")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.Property<string>("Title")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(null);

                    b.HasKey("Id")
                        .HasName("PKEY_PackageId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("SlimGet.Data.Database.PackageAuthor", b =>
                {
                    b.Property<string>("PackageId");

                    b.Property<string>("Name");

                    b.HasKey("PackageId", "Name")
                        .HasName("PKEY_Author");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("SlimGet.Data.Database.PackageDependency", b =>
                {
                    b.Property<string>("PackageId");

                    b.Property<string>("PackageVersion");

                    b.Property<string>("Id");

                    b.Property<string>("TargetFramework");

                    b.Property<string>("VersionRange")
                        .IsRequired();

                    b.HasKey("PackageId", "PackageVersion", "Id", "TargetFramework")
                        .HasName("PKEY_Dependency");

                    b.ToTable("Dependencies");
                });

            modelBuilder.Entity("SlimGet.Data.Database.PackageTag", b =>
                {
                    b.Property<string>("PackageId");

                    b.Property<string>("Tag");

                    b.HasKey("PackageId", "Tag")
                        .HasName("PKEY_Tag");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("SlimGet.Data.Database.PackageVersion", b =>
                {
                    b.Property<string>("PackageId");

                    b.Property<string>("Version");

                    b.Property<long>("DownloadCount")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0L);

                    b.Property<bool>("IsListed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsPrerelase")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("PublishedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("PackageId", "Version")
                        .HasName("PKEY_Version");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("SlimGet.Data.Database.PackageAuthor", b =>
                {
                    b.HasOne("SlimGet.Data.Database.Package", "Package")
                        .WithMany("Authors")
                        .HasForeignKey("PackageId")
                        .HasConstraintName("FKEY_Author_PackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SlimGet.Data.Database.PackageDependency", b =>
                {
                    b.HasOne("SlimGet.Data.Database.PackageVersion", "Package")
                        .WithMany("Dependencies")
                        .HasForeignKey("PackageId", "PackageVersion")
                        .HasConstraintName("FKEY_Dependency_PackageId_PackageVersion")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SlimGet.Data.Database.PackageTag", b =>
                {
                    b.HasOne("SlimGet.Data.Database.Package", "Package")
                        .WithMany("Tags")
                        .HasForeignKey("PackageId")
                        .HasConstraintName("FKEY_Tag_PackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SlimGet.Data.Database.PackageVersion", b =>
                {
                    b.HasOne("SlimGet.Data.Database.Package", "Package")
                        .WithMany("Versions")
                        .HasForeignKey("PackageId")
                        .HasConstraintName("FKEY_Version_PackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
