﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.FundooContext;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(FundoosContext))]
    [Migration("20220512180259_NoteTable")]
    partial class NoteTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepositoryLayer.Entities.Label", b =>
                {
                    b.Property<int>("labelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NoteId")
                        .HasColumnType("int");

                    b.Property<string>("labelName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("userId")
                        .HasColumnType("int");

                    b.HasKey("labelId");

                    b.HasIndex("NoteId");

                    b.HasIndex("userId");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.Note", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsArchieve")
                        .HasColumnType("bit");

                    b.Property<bool>("IsReminder")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTrash")
                        .HasColumnType("bit");

                    b.Property<bool>("Ispin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReminderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("NoteId");

                    b.HasIndex("userId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.User", b =>
                {
                    b.Property<int>("userId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("registeredDate")
                        .HasColumnType("datetime2");

                    b.HasKey("userId");

                    b.HasIndex("email")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.ToTable("User");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.Label", b =>
                {
                    b.HasOne("RepositoryLayer.Entities.Note", "Note")
                        .WithMany("Labels")
                        .HasForeignKey("NoteId");

                    b.HasOne("RepositoryLayer.Entities.User", "User")
                        .WithMany("Labels")
                        .HasForeignKey("userId");

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.Note", b =>
                {
                    b.HasOne("RepositoryLayer.Entities.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.Note", b =>
                {
                    b.Navigation("Labels");
                });

            modelBuilder.Entity("RepositoryLayer.Entities.User", b =>
                {
                    b.Navigation("Labels");

                    b.Navigation("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}
