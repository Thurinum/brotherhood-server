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

            modelBuilder.Entity("AssassinContract", b =>
                {
                    b.Property<string>("AssassinsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ContractsId")
                        .HasColumnType("int");

                    b.HasKey("AssassinsId", "ContractsId");

                    b.HasIndex("ContractsId");

                    b.ToTable("AssassinContract");

                    b.HasData(
                        new
                        {
                            AssassinsId = "11111111-1111-1111-1111-111111111111",
                            ContractsId = 1
                        },
                        new
                        {
                            AssassinsId = "11111111-1111-1111-1111-111111111111",
                            ContractsId = 2
                        },
                        new
                        {
                            AssassinsId = "69696969-6969-6969-6969-696969696969",
                            ContractsId = 3
                        },
                        new
                        {
                            AssassinsId = "11111111-1111-1111-1111-111111111111",
                            ContractsId = 4
                        },
                        new
                        {
                            AssassinsId = "96969696-9696-9696-9696-969696969696",
                            ContractsId = 5
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

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

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
                            ConcurrencyStamp = "85bdc95b-43f3-431a-a3ef-7039eb1f8531",
                            Email = "ezio.auditore@firenze.it",
                            EmailConfirmed = false,
                            FirstName = "Ezio",
                            LastName = "Auditore",
                            LockoutEnabled = false,
                            NormalizedEmail = "EZIO.AUDITORE@FIRENZE.IT",
                            NormalizedUserName = "EZIO",
                            PasswordHash = "AQAAAAEAACcQAAAAEMwKXyY54FBWk+5Ov7SFs8HL551vDaRySnk8mO3whfRdEjWrUHV2BLbtdvcUNzKQBg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "addfe49f-ffa6-43b8-9e77-3f07c4d04f39",
                            TwoFactorEnabled = false,
                            UserName = "Ezio"
                        },
                        new
                        {
                            Id = "69696969-6969-6969-6969-696969696969",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b06e4d75-9786-40cd-82a4-6e4dab2ccbe9",
                            Email = "arno.dorian@brotherhood.fr",
                            EmailConfirmed = false,
                            FirstName = "Arno",
                            LastName = "Dorian",
                            LockoutEnabled = false,
                            NormalizedEmail = "ARNO.DORIAN@BROTHERHOOD.fr",
                            NormalizedUserName = "ARNO",
                            PasswordHash = "AQAAAAEAACcQAAAAECqyuqxc30N9POc3A/bBnFU8XdFSUv0JggBeT+N3pHkUzb8ORKLsmZnDhxR2gYfOVw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "bca832ca-1dd0-4501-9bd7-46ea436f61c9",
                            TwoFactorEnabled = false,
                            UserName = "Arno"
                        },
                        new
                        {
                            Id = "96969696-9696-9696-9696-969696969696",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "5dbc2d7e-761a-480b-bf2a-43470e2a8eb2",
                            Email = "theodore.lheureux@archlinux.net",
                            EmailConfirmed = false,
                            FirstName = "Theodore",
                            LastName = "l'Heureux",
                            LockoutEnabled = false,
                            NormalizedEmail = "THEODORE.LHEUREUX@ARCHLINUX.NET",
                            NormalizedUserName = "THEODORE",
                            PasswordHash = "AQAAAAEAACcQAAAAEI+U9idSpbKoh19tMbgHh4ynqDODALcHLtks+ar5Xk/9lSdP4ZwrwpYa1wGjgX49Ew==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a13f04ea-1fe0-4faf-bdd9-2cff0566ca51",
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

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "Italy",
                            Name = "Florence"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Italy",
                            Name = "Rome"
                        },
                        new
                        {
                            Id = 3,
                            Country = "France",
                            Name = "Paris"
                        },
                        new
                        {
                            Id = 4,
                            Country = "Italy",
                            Name = "Venice"
                        },
                        new
                        {
                            Id = 5,
                            Country = "Canada",
                            Name = "Longueuil"
                        });
                });

            modelBuilder.Entity("Brotherhood_Server.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Briefing")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Codename")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("FeaturedContractId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Contracts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Briefing = "The dastarly Haytham Kenway is disrupting peace and hindering freedom of the people of America. Bring him and his acolytes down and ensure justice is brought to the people.",
                            CityId = 5,
                            Codename = "Codename: LoneEagle",
                            IsPublic = true
                        },
                        new
                        {
                            Id = 2,
                            Briefing = "The Pope is a threat to the people of Italy. Bring him down and ensure justice is brought to the people.",
                            CityId = 2,
                            Codename = "Pope",
                            IsPublic = true
                        },
                        new
                        {
                            Id = 3,
                            Briefing = "The dastarly Shay Cormac is disrupting peace and hindering freedom of the people of America. Bring him down and ensure justice is brought to the people.",
                            CityId = 5,
                            Codename = "Rogue",
                            IsPublic = true
                        },
                        new
                        {
                            Id = 4,
                            Briefing = "The dastarly Charles lee is disrupting peace and hindering freedom of the people of America. Bring him down and ensure justice is brought to the people.",
                            CityId = 5,
                            Codename = "Dastardly",
                            IsPublic = true
                        },
                        new
                        {
                            Id = 5,
                            Briefing = "The dastarly Julie Pro is disrupting peace and hindering freedom of the people of America. Bring him down and ensure justice is brought to the people.",
                            CityId = 3,
                            Codename = "ViewModel",
                            IsPublic = false
                        });
                });

            modelBuilder.Entity("Brotherhood_Server.Models.ContractTarget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.ToTable("Targets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContractId = 5,
                            FirstName = "Haytham",
                            LastName = "Kenway"
                        },
                        new
                        {
                            Id = 2,
                            ContractId = 2,
                            FirstName = "Rodrigo",
                            LastName = "Borgia"
                        },
                        new
                        {
                            Id = 3,
                            ContractId = 5,
                            FirstName = "Shay",
                            LastName = "Cormac"
                        },
                        new
                        {
                            Id = 4,
                            ContractId = 5,
                            FirstName = "Charles",
                            LastName = "Lee"
                        },
                        new
                        {
                            Id = 5,
                            ContractId = 1,
                            FirstName = "Valerie",
                            LastName = "Turgeon"
                        },
                        new
                        {
                            Id = 6,
                            ContractId = 1,
                            FirstName = "Valerie",
                            LastName = "Turgeon"
                        },
                        new
                        {
                            Id = 7,
                            ContractId = 1,
                            FirstName = "Valerie",
                            LastName = "Turgeon"
                        },
                        new
                        {
                            Id = 8,
                            ContractId = 1,
                            FirstName = "Valerie",
                            LastName = "Turgeon"
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

            modelBuilder.Entity("AssassinContract", b =>
                {
                    b.HasOne("Brotherhood_Server.Models.Assassin", null)
                        .WithMany()
                        .HasForeignKey("AssassinsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brotherhood_Server.Models.Contract", null)
                        .WithMany()
                        .HasForeignKey("ContractsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Brotherhood_Server.Models.ContractTarget", b =>
                {
                    b.HasOne("Brotherhood_Server.Models.Contract", null)
                        .WithMany("Targets")
                        .HasForeignKey("ContractId")
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

            modelBuilder.Entity("Brotherhood_Server.Models.Contract", b =>
                {
                    b.Navigation("Targets");
                });
#pragma warning restore 612, 618
        }
    }
}
