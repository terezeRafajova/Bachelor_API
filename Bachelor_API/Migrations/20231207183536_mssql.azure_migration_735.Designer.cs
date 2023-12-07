﻿// <auto-generated />
using System;
using Bachelor_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bachelor_API.Migrations
{
    [DbContext(typeof(Bachelor_APIContext))]
    [Migration("20231207183536_mssql.azure_migration_735")]
    partial class mssqlazure_migration_735
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bachelor_API.Model.CodeBlock", b =>
                {
                    b.Property<int>("CodeBlockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CodeBlockId"));

                    b.Property<string>("JsonBlocks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("LessonId")
                        .HasColumnType("int");

                    b.Property<int?>("PageNumber")
                        .HasColumnType("int");

                    b.Property<int?>("Slot")
                        .HasColumnType("int");

                    b.HasKey("CodeBlockId");

                    b.HasIndex("LessonId");

                    b.ToTable("CodeBlock");
                });

            modelBuilder.Entity("Bachelor_API.Model.Description", b =>
                {
                    b.Property<int>("DescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DescriptionId"));

                    b.Property<int?>("LessonId")
                        .HasColumnType("int");

                    b.Property<int?>("PageNumber")
                        .HasColumnType("int");

                    b.Property<int?>("Slot")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DescriptionId");

                    b.HasIndex("LessonId");

                    b.ToTable("Description");
                });

            modelBuilder.Entity("Bachelor_API.Model.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LessonId"));

                    b.Property<int?>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<int?>("SharingCode")
                        .HasColumnType("int");

                    b.Property<int?>("SharingTime")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LessonId");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("Bachelor_API.Model.Teacher", b =>
                {
                    b.Property<int>("TeacherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeacherId"));

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeacherId");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Bachelor_API.Model.Title", b =>
                {
                    b.Property<int>("TitleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TitleId"));

                    b.Property<int?>("LessonId")
                        .HasColumnType("int");

                    b.Property<int?>("PageNumber")
                        .HasColumnType("int");

                    b.Property<int?>("Slot")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TitleId");

                    b.HasIndex("LessonId");

                    b.ToTable("Title");
                });

            modelBuilder.Entity("Bachelor_API.Model.CodeBlock", b =>
                {
                    b.HasOne("Bachelor_API.Model.Lesson", null)
                        .WithMany("CodeBlocks")
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("Bachelor_API.Model.Description", b =>
                {
                    b.HasOne("Bachelor_API.Model.Lesson", null)
                        .WithMany("Descriptions")
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("Bachelor_API.Model.Title", b =>
                {
                    b.HasOne("Bachelor_API.Model.Lesson", null)
                        .WithMany("Titles")
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("Bachelor_API.Model.Lesson", b =>
                {
                    b.Navigation("CodeBlocks");

                    b.Navigation("Descriptions");

                    b.Navigation("Titles");
                });
#pragma warning restore 612, 618
        }
    }
}
