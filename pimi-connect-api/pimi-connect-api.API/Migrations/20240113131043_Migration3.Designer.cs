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
    [Migration("20240113131043_Migration3")]
    partial class Migration3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("pimi_connect_app.Data.Entities.AuthEntity", b =>
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

                    b.Property<byte[]>("PrivateKey")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("Auths");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.ChatEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ThumbnailId")
                        .HasColumnType("uuid");

                    b.Property<int>("g")
                        .HasColumnType("integer");

                    b.Property<int>("p")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ThumbnailId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.ChatPasswordEntity", b =>
                {
                    b.Property<Guid>("PasswordContainerId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.ToTable("ChatPasswords");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.ChatThumbnailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("publicName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ChatThumbnails");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.EmailEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Template")
                        .HasColumnType("integer");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.MessageAttachmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("MessageEntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("publicName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MessageEntityId");

                    b.ToTable("MessageAttachments");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.MessageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AttachmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PasswordContainerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserCreatedId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("PasswordContainerId");

                    b.HasIndex("UserCreatedId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.PasswordContainerEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("PasswordContainers");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.ProfilePictureEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("publicName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ProfilePictures");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.UserChatEntity", b =>
                {
                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("LastReadMessageId")
                        .HasColumnType("uuid");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserKeyId")
                        .HasColumnType("uuid");

                    b.HasIndex("ChatId");

                    b.HasIndex("LastReadMessageId");

                    b.HasIndex("UserId");

                    b.ToTable("UserChats");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("ProfilePictureId")
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

            modelBuilder.Entity("pimi_connect_app.Data.Entities.UserKeyEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("IndirectKey")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.ToTable("UserKeys");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.ChatEntity", b =>
                {
                    b.HasOne("pimi_connect_app.Data.Entities.ChatThumbnailEntity", "Thumbnail")
                        .WithMany()
                        .HasForeignKey("ThumbnailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thumbnail");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.MessageAttachmentEntity", b =>
                {
                    b.HasOne("pimi_connect_app.Data.Entities.MessageEntity", null)
                        .WithMany("Attachments")
                        .HasForeignKey("MessageEntityId");
                });

            modelBuilder.Entity("pimi_connect_app.Data.Entities.MessageEntity", b =>
                {
                    b.HasOne("pimi_connect_app.Data.Entities.ChatEntity", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pimi_connect_app.Data.Entities.PasswordContainerEntity", "PasswordContainer")
                        .WithMany()
                        .HasForeignKey("PasswordContainerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("pimi_connect_app.Data.Entities.UserEntity", "UserCreated")
                        .WithMany("Messages")
                        .HasForeignKey("UserCreatedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("PasswordContainer");

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

                    b.HasOne("pimi_connect_app.Data.Entities.ProfilePictureEntity", "ProfilePicture")
                        .WithMany()
                        .HasForeignKey("ProfilePictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

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

            modelBuilder.Entity("pimi_connect_app.Data.Entities.UserEntity", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
