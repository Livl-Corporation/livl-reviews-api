﻿// <auto-generated />
using System;
using LivlReviews.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LivlReviews.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240604003654_UpdateReceivedAtToReviewableAtInReviewTable")]
    partial class UpdateReceivedAtToReviewableAtInReviewTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LivlReviews.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.InvitationToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("InvitedByUserId")
                        .HasColumnType("text");

                    b.Property<string>("InvitedUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InvitedByUserId");

                    b.HasIndex("InvitedUserId")
                        .IsUnique();

                    b.ToTable("InvitationTokens");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SourcePage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.ProductStock", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("AdminId")
                        .HasColumnType("text");

                    b.HasKey("ProductId", "AdminId");

                    b.HasIndex("AdminId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AdminId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AdminMessage")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("ReviewableAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserMessage")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("character varying(10000)");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<int>("RequestId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("LivlReviews.Infra.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int?>("InvitedByTokenId")
                        .HasColumnType("integer");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Category", b =>
                {
                    b.HasOne("LivlReviews.Domain.Entities.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.InvitationToken", b =>
                {
                    b.HasOne("LivlReviews.Infra.Data.User", "InvitedByUser")
                        .WithMany("CreatedInvitationTokens")
                        .HasForeignKey("InvitedByUserId");

                    b.HasOne("LivlReviews.Infra.Data.User", "InvitedUser")
                        .WithOne("InvitedByToken")
                        .HasForeignKey("LivlReviews.Domain.Entities.InvitationToken", "InvitedUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvitedByUser");

                    b.Navigation("InvitedUser");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Product", b =>
                {
                    b.HasOne("LivlReviews.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.ProductStock", b =>
                {
                    b.HasOne("LivlReviews.Infra.Data.User", "Admin")
                        .WithMany("Stocks")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LivlReviews.Domain.Entities.Product", "Product")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Request", b =>
                {
                    b.HasOne("LivlReviews.Infra.Data.User", "Admin")
                        .WithMany("ReceivedRequests")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("LivlReviews.Domain.Entities.Product", "Product")
                        .WithMany("Requests")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("LivlReviews.Infra.Data.User", "User")
                        .WithMany("SubmittedRequests")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Review", b =>
                {
                    b.HasOne("LivlReviews.Domain.Entities.Request", "Request")
                        .WithMany()
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LivlReviews.Infra.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LivlReviews.Infra.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("LivlReviews.Infra.Data.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Category", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("LivlReviews.Domain.Entities.Product", b =>
                {
                    b.Navigation("Requests");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("LivlReviews.Infra.Data.User", b =>
                {
                    b.Navigation("CreatedInvitationTokens");

                    b.Navigation("InvitedByToken");

                    b.Navigation("ReceivedRequests");

                    b.Navigation("Stocks");

                    b.Navigation("SubmittedRequests");
                });
#pragma warning restore 612, 618
        }
    }
}
