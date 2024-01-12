﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pimi_connect_app.Data.AppDbContext;

#nullable disable

namespace pimi_connect_api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240102142702_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("pimi_connect_app.Data.Entities.AttachmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("MessageEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ObjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MessageEntityId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.AuthEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("AuthEntities");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.ChatEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("ThumbnailId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ThumbnailKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("g")
                        .HasColumnType("int");

                    b.Property<int>("p")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ThumbnailId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.MessageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AttachmentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("IdPasswordContainer")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserCreatedId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.UserChatEntity", b =>
                {
                    b.Property<Guid>("ChatId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LastReadMessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserKeyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasIndex("ChatId");

                    b.HasIndex("LastReadMessageId");

                    b.HasIndex("UserId");

                    b.ToTable("UserChats");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<Guid?>("ProfilePictureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProfilePictureKey")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AuthId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ProfilePictureId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.AttachmentEntity", b =>
                {
                    b.HasOne("pimi_connect_app.Data.Entities.MessageEntity", null)
                        .WithMany("Attachments")
                        .HasForeignKey("MessageEntityId");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.ChatEntity", b =>
                {
                    b.HasOne("pimi_connect_app.Data.Entities.AttachmentEntity", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailId");

                    b.Navigation("Thumbnail");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.MessageEntity", b =>
                {
                    b.HasOne("pimi_connect_app.Data.Entities.ChatEntity", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pimi_connect_app.Data.Entities.UserEntity", "UserCreated")
                        .WithMany()
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("UserCreated");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.UserChatEntity", b =>
                {
                    b.HasOne("pimi_connect_app.Data.Entities.ChatEntity", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pimi_connect_app.Data.Entities.MessageEntity", "LastReadMessage")
                        .WithMany()
                        .HasForeignKey("LastReadMessageId");

                    b.HasOne("pimi_connect_app.Data.Entities.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("LastReadMessage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.UserEntity", b =>
                {
                    b.HasOne("pimi_connect_app.Data.Entities.AuthEntity", "Auth")
                        .WithMany()
                        .HasForeignKey("AuthId");

                    b.HasOne("pimi_connect_app.Data.Entities.AttachmentEntity", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId");

                    b.Navigation("Auth");

                    b.Navigation("ProfilePicture");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.ChatEntity", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.MessageEntity", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}