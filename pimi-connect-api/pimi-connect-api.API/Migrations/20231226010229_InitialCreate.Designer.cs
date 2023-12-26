﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using pimi_connect_app.Data.AppDbContext;

#nullable disable

namespace pimi_connect_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231226010229_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.AuthEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("AuthEntities");
                });

            modelBuilder.Entity("Models.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("ProfilePictureId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProfilePictureKey")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("AuthId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.AttachmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Models.UserChatEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserEntityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserEntityId");

                    b.ToTable("UserChats");
                });

            modelBuilder.Entity("Models.UserEntity", b =>
                {
                    b.HasOne("Models.AuthEntity", "Auth")
                        .WithMany()
                        .HasForeignKey("AuthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pimi_connect_app.Data.Entities.AttachmentEntity", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auth");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Models.UserChatEntity", b =>
                {
                    b.HasOne("Models.UserEntity", null)
                        .WithMany("UserChats")
                        .HasForeignKey("UserEntityId");
                });

            modelBuilder.Entity("Models.UserEntity", b =>
                {
                    b.Navigation("UserChats");
                });
#pragma warning restore 612, 618
        }
    }
}
