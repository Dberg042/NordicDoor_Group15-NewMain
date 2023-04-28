﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NordicDoor_Group15.Areas.Identity.Data;

#nullable disable

namespace NordicDoor_Group15.Data.IDMigrations
{
    [DbContext(typeof(ApplicationIdentityDbContext))]
    partial class ApplicationIdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NordicDoor_Group15.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("EmployeeNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.FileContent", b =>
                {
                    b.Property<int>("FileContentID")
                        .HasColumnType("int");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.HasKey("FileContentID");

                    b.ToTable("FileContent");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Membership", b =>
                {
                    b.Property<int>("TeamID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("varchar(255)");

                    b.HasKey("TeamID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("Memberships");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PhotoName")
                        .HasColumnType("longtext");

                    b.Property<int>("SuggestionID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("SuggestionID");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Suggestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatorID")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(180)
                        .HasColumnType("varchar(180)");

                    b.Property<string>("MainBody")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Status")
                        .HasColumnType("longtext");

                    b.Property<int?>("TeamID")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorID");

                    b.HasIndex("TeamID");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.SuggestionPhoto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SuggestionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SuggestionID")
                        .IsUnique();

                    b.ToTable("SuggestionPhotos");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.SuggestionThumbnail", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte[]>("Content")
                        .HasColumnType("longblob");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SuggestionID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SuggestionID")
                        .IsUnique();

                    b.ToTable("SuggestionThumbnails");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Team", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("CreatedOn")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("timestamp(6)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TeamNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.UploadedFile", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FileName")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ID");

                    b.ToTable("UploadedFiles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UploadedFile");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.SuggestionDocument", b =>
                {
                    b.HasBaseType("NordicDoor_Group15.Models.UploadedFile");

                    b.Property<int>("SuggestionID")
                        .HasColumnType("int");

                    b.HasIndex("SuggestionID");

                    b.HasDiscriminator().HasValue("SuggestionDocument");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NordicDoor_Group15.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NordicDoor_Group15.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NordicDoor_Group15.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NordicDoor_Group15.Areas.Identity.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.FileContent", b =>
                {
                    b.HasOne("NordicDoor_Group15.Models.UploadedFile", "UploadedFile")
                        .WithOne("FileContent")
                        .HasForeignKey("NordicDoor_Group15.Models.FileContent", "FileContentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UploadedFile");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Membership", b =>
                {
                    b.HasOne("NordicDoor_Group15.Models.Team", "Team")
                        .WithMany("Memberships")
                        .HasForeignKey("TeamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NordicDoor_Group15.Areas.Identity.Data.ApplicationUser", "User")
                        .WithMany("Memberships")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Photo", b =>
                {
                    b.HasOne("NordicDoor_Group15.Models.Suggestion", "Suggestion")
                        .WithMany("Photos")
                        .HasForeignKey("SuggestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Suggestion", b =>
                {
                    b.HasOne("NordicDoor_Group15.Areas.Identity.Data.ApplicationUser", "Creator")
                        .WithMany("Suggestions")
                        .HasForeignKey("CreatorID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("NordicDoor_Group15.Models.Team", "Team")
                        .WithMany("Suggestions")
                        .HasForeignKey("TeamID");

                    b.Navigation("Creator");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.SuggestionPhoto", b =>
                {
                    b.HasOne("NordicDoor_Group15.Models.Suggestion", "Suggestion")
                        .WithOne("SuggestionPhoto")
                        .HasForeignKey("NordicDoor_Group15.Models.SuggestionPhoto", "SuggestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.SuggestionThumbnail", b =>
                {
                    b.HasOne("NordicDoor_Group15.Models.Suggestion", "Suggestion")
                        .WithOne("SuggestionThumbnail")
                        .HasForeignKey("NordicDoor_Group15.Models.SuggestionThumbnail", "SuggestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.SuggestionDocument", b =>
                {
                    b.HasOne("NordicDoor_Group15.Models.Suggestion", "Suggestion")
                        .WithMany()
                        .HasForeignKey("SuggestionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suggestion");
                });

            modelBuilder.Entity("NordicDoor_Group15.Areas.Identity.Data.ApplicationUser", b =>
                {
                    b.Navigation("Memberships");

                    b.Navigation("Suggestions");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Suggestion", b =>
                {
                    b.Navigation("Photos");

                    b.Navigation("SuggestionPhoto");

                    b.Navigation("SuggestionThumbnail");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.Team", b =>
                {
                    b.Navigation("Memberships");

                    b.Navigation("Suggestions");
                });

            modelBuilder.Entity("NordicDoor_Group15.Models.UploadedFile", b =>
                {
                    b.Navigation("FileContent");
                });
#pragma warning restore 612, 618
        }
    }
}
