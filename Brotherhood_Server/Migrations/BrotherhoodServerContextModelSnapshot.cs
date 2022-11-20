﻿// <auto-generated />
using System;
using Brotherhood_Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Brotherhood_Server.Migrations
{
    [DbContext(typeof(BrotherhoodServerContext))]
    partial class BrotherhoodServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssassinCity", b =>
                {
                    b.Property<string>("AssassinsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CitiesId")
                        .HasColumnType("int");

                    b.HasKey("AssassinsId", "CitiesId");

                    b.HasIndex("CitiesId");

                    b.ToTable("AssassinCity");

                    b.HasData(
                        new
                        {
                            AssassinsId = "11111111-1111-1111-1111-111111111111",
                            CitiesId = 1
                        },
                        new
                        {
                            AssassinsId = "11111111-1111-1111-1111-111111111111",
                            CitiesId = 2
                        },
                        new
                        {
                            AssassinsId = "69696969-6969-6969-6969-696969696969",
                            CitiesId = 3
                        },
                        new
                        {
                            AssassinsId = "11111111-1111-1111-1111-111111111111",
                            CitiesId = 4
                        },
                        new
                        {
                            AssassinsId = "96969696-9696-9696-9696-969696969696",
                            CitiesId = 5
                        });
                });

            modelBuilder.Entity("Brotherhood_Server.Models.Assassin", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "11111111-1111-1111-1111-111111111111",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "734fed8e-ecf7-4b69-b7e0-55d0b3d15fa2",
                            Email = "ezio.auditore@firenze.it",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "EZIO.AUDITORE@FIRENZE.IT",
                            NormalizedUserName = "EZIO",
                            PasswordHash = "AQAAAAEAACcQAAAAEPIhphdi3udgU4xxNabbehlHba732TgB/oooaPo6nyhyBSZ9L5lIusmhB0HZLwxDeQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "fca5c500-6de8-42eb-b8b5-30b4846e70ba",
                            TwoFactorEnabled = false,
                            UserName = "Ezio"
                        },
                        new
                        {
                            Id = "69696969-6969-6969-6969-696969696969",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9c12f1f4-b2cd-4b28-9c07-e433abbfdad0",
                            Email = "arno.dorian@brotherhood.org",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ARNO.DORIAN@BROTHERHOOD.ORG",
                            NormalizedUserName = "ARNO",
                            PasswordHash = "AQAAAAEAACcQAAAAEDLN8SdO2g2h50cCXIANccUQjMRGe3rIATHwYkqrPChaQkFnBm+zP6+HZZLch9yJ+w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3c3c08ff-e817-45b1-9727-6a9edf1ff480",
                            TwoFactorEnabled = false,
                            UserName = "Arno"
                        },
                        new
                        {
                            Id = "96969696-9696-9696-9696-969696969696",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e6195a8d-532b-4538-a32b-4b9d8a66c3d2",
                            Email = "theodore.lheureux@archlinux.net",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "THEODORE.LHEUREUX@ARCHLINUX.NET",
                            NormalizedUserName = "THEODORE",
                            PasswordHash = "AQAAAAEAACcQAAAAEE4SoN3dVFPd+xmDR638UACFWzX1k30WrKDgYLnpBTqb7/bzmNZ7leBCNFB3Wgza3g==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9b4ace7b-d8f9-49f6-a8ce-d200afe0de0a",
                            TwoFactorEnabled = false,
                            UserName = "Theodore"
                        });
                });

            modelBuilder.Entity("Brotherhood_Server.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("City");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsPublic = true,
                            Name = "Florence"
                        },
                        new
                        {
                            Id = 2,
                            IsPublic = true,
                            Name = "Rome"
                        },
                        new
                        {
                            Id = 3,
                            IsPublic = true,
                            Name = "Paris"
                        },
                        new
                        {
                            Id = 4,
                            IsPublic = true,
                            Name = "Venice"
                        },
                        new
                        {
                            Id = 5,
                            IsPublic = false,
                            Name = "Longueuil"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AssassinCity", b =>
                {
                    b.HasOne("Brotherhood_Server.Models.Assassin", null)
                        .WithMany()
                        .HasForeignKey("AssassinsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brotherhood_Server.Models.City", null)
                        .WithMany()
                        .HasForeignKey("CitiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("Brotherhood_Server.Models.Assassin", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Brotherhood_Server.Models.Assassin", null)
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

                    b.HasOne("Brotherhood_Server.Models.Assassin", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Brotherhood_Server.Models.Assassin", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
