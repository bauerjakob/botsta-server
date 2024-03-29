﻿// <auto-generated />
using System;
using Botsta.DataStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Botsta.DataStorage.Migrations
{
    [DbContext(typeof(BotstaDbContext))]
    [Migration("20210704155902_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Botsta.DataStorage.Entities.Bot", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Bots");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.ChatPracticant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Registerd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecretHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecretSalt")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ChatPracticant");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.Chatroom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Chatroom");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.KeyExchange", b =>
                {
                    b.Property<Guid>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatPracticantId")
                        .HasColumnType("uuid");

                    b.Property<string>("PublicKey")
                        .HasColumnType("text");

                    b.HasKey("SessionId");

                    b.HasIndex("ChatPracticantId");

                    b.ToTable("KeyExchanges");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatroomId")
                        .HasColumnType("uuid");

                    b.Property<string>("Msg")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Message");

                    b.Property<Guid>("ReceiverSessionId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("SendTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<string>("SenderPublicKey")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChatroomId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ChatPracticantChatroom", b =>
                {
                    b.Property<Guid>("ChatPracticantsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatroomsId")
                        .HasColumnType("uuid");

                    b.HasKey("ChatPracticantsId", "ChatroomsId");

                    b.HasIndex("ChatroomsId");

                    b.ToTable("ChatPracticantChatroom");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.Bot", b =>
                {
                    b.HasOne("Botsta.DataStorage.Entities.ChatPracticant", "ChatPracticant")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Botsta.DataStorage.Entities.User", "Owner")
                        .WithMany("Bots")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatPracticant");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.KeyExchange", b =>
                {
                    b.HasOne("Botsta.DataStorage.Entities.ChatPracticant", "ChatPracticant")
                        .WithMany("KeyExchange")
                        .HasForeignKey("ChatPracticantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatPracticant");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.Message", b =>
                {
                    b.HasOne("Botsta.DataStorage.Entities.Chatroom", "Chatroom")
                        .WithMany("Messages")
                        .HasForeignKey("ChatroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Botsta.DataStorage.Entities.ChatPracticant", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chatroom");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.User", b =>
                {
                    b.HasOne("Botsta.DataStorage.Entities.ChatPracticant", "ChatPracticant")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatPracticant");
                });

            modelBuilder.Entity("ChatPracticantChatroom", b =>
                {
                    b.HasOne("Botsta.DataStorage.Entities.ChatPracticant", null)
                        .WithMany()
                        .HasForeignKey("ChatPracticantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Botsta.DataStorage.Entities.Chatroom", null)
                        .WithMany()
                        .HasForeignKey("ChatroomsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.ChatPracticant", b =>
                {
                    b.Navigation("KeyExchange");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.Chatroom", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Botsta.DataStorage.Entities.User", b =>
                {
                    b.Navigation("Bots");
                });
#pragma warning restore 612, 618
        }
    }
}
