﻿// <auto-generated />
using System;
using MicroSAuth_GUser.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MicroServiceMessagerie.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240613194128_messages")]
    partial class messages
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ServiceMessagerie.Models.Conversation", b =>
                {
                    b.Property<string>("idconversation")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("idconversation");

                    b.ToTable("Conversation", (string)null);
                });

            modelBuilder.Entity("ServiceMessagerie.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contenu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Emeteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recepteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("conversationidconversation")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("conversationidconversation");

                    b.ToTable("Message", (string)null);
                });

            modelBuilder.Entity("ServiceMessagerie.Models.Message", b =>
                {
                    b.HasOne("ServiceMessagerie.Models.Conversation", "conversation")
                        .WithMany("Messages")
                        .HasForeignKey("conversationidconversation");

                    b.Navigation("conversation");
                });

            modelBuilder.Entity("ServiceMessagerie.Models.Conversation", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
