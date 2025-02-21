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
    [Migration("20250221130031_SeedDatabase")]
    partial class SeedDatabase
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

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Balance = 1500.00m,
                            Name = "Checking Account",
                            Type = 0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Balance = 5000.00m,
                            Name = "Savings Account",
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

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountId = 1,
                            Amount = -100.00m,
                            Date = new DateTime(2025, 2, 21, 13, 0, 31, 52, DateTimeKind.Utc).AddTicks(9460),
                            Description = "Groceries",
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            AccountId = 2,
                            Amount = 500.00m,
                            Date = new DateTime(2025, 2, 21, 13, 0, 31, 52, DateTimeKind.Utc).AddTicks(9584),
                            Description = "Salary",
                            Type = 0
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
                            PasswordHash = "AQAAAAIAAYagAAAAEANYsqKzwes8Qz/34KuFwSvDnrY+cqvtNfIDXBUx+uV83B4anF3GnnIIMiMi69Xvgw==",
                            Username = "John Doe"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane@example.com",
                            PasswordHash = "AQAAAAIAAYagAAAAEPlJlMMWnEsmbjoSL8g2QVH1D5OLGH1imh9gkGcYotVfFFyQUNWLUt5/do3L0hgDPg==",
                            Username = "Jane Smith"
                        });
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.Account", b =>
                {
                    b.HasOne("FinanceManagerAPI.Models.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.Transaction", b =>
                {
                    b.HasOne("FinanceManagerAPI.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FinanceManagerAPI.Models.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
