﻿// <auto-generated />
using System;
using FinanceManagerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250307132308_TestNavigationPropertiesListAccount4")]
    partial class TestNavigationPropertiesListAccount4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("FinanceManagerAPI.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Balance")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId1")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Balance = 1500.00m,
                            Name = "CheckingAccount",
                            Type = 0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Balance = 5000.00m,
                            Name = "SavingsAccount",
                            Type = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Balance = 200.00m,
                            Name = "Cash",
                            Type = 3,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            Amount = -100.00m,
                            Date = new DateTime(2025, 5, 5, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Groceries",
                            Type = 1,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 2,
                            Amount = 500.00m,
                            Date = new DateTime(2025, 4, 4, 9, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Salary",
                            Type = 0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            AccountId = 3,
                            Amount = 500.00m,
                            Date = new DateTime(2025, 3, 3, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "CashGift",
                            Type = 0,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john@example.com",
                            PasswordHash = "AQAAAAIAAYagAAAAEIVKrR1L8tvsU/NMS4cgtrrT2ax2Mtvay+gzmr2m8eE79SZcwFfoe7ZQbnPKclNb9A==",
                            Username = "John Doe"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane@example.com",
                            PasswordHash = "AQAAAAIAAYagAAAAEGYecMZnk0LKxGafJxH017RgS10W9U4orGUFbUUfYwNHFniaU4xcIcYqSb0utNolJA==",
                            Username = "Jane Smith"
                        });
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.Account", b =>
                {
                    b.HasOne("FinanceManagerAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinanceManagerAPI.Models.User", null)
                        .WithMany("Accounts")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.Transaction", b =>
                {
                    b.HasOne("FinanceManagerAPI.Models.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
