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
                            AssassinsId = "96969696-9696-9696-9696-969696969696",
                            ContractsId = 1
                        },
                        new
                        {
                            AssassinsId = "69696969-6969-6969-6969-696969696969",
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
                            ConcurrencyStamp = "25a3edbc-7782-4fca-9409-b68d98c28933",
                            Email = "ezio.auditore@firenze.it",
                            EmailConfirmed = false,
                            FirstName = "Ezio",
                            LastName = "Auditore",
                            LockoutEnabled = false,
                            NormalizedEmail = "EZIO.AUDITORE@FIRENZE.IT",
                            NormalizedUserName = "EZIO",
                            PasswordHash = "AQAAAAEAACcQAAAAEHqyCqvFR485cj/loKhsa/uQDghseiEk5ixdSK9fYSNO8ZL8HifGjrdVDDlkICEfeA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "805647e1-9bb5-453b-9250-f79438f41edf",
                            TwoFactorEnabled = false,
                            UserName = "Ezio"
                        },
                        new
                        {
                            Id = "69696969-6969-6969-6969-696969696969",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b3f6ee4b-7bfe-45c8-b102-68249f9ef64b",
                            Email = "arno.dorian@brotherhood.fr",
                            EmailConfirmed = false,
                            FirstName = "Arno",
                            LastName = "Dorian",
                            LockoutEnabled = false,
                            NormalizedEmail = "ARNO.DORIAN@BROTHERHOOD.fr",
                            NormalizedUserName = "ARNO",
                            PasswordHash = "AQAAAAEAACcQAAAAEBe2spaoLXDhfWP35a9jWpMk0Koh7ACF97MYDYeznwlbPH/+ELbI+GGOZAy5a1m6RQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5cb6cd0e-bd41-4a08-9016-3e4d658c6414",
                            TwoFactorEnabled = false,
                            UserName = "Arno"
                        },
                        new
                        {
                            Id = "96969696-9696-9696-9696-969696969696",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2084d6fb-1c78-482f-9f0f-58de6a99436f",
                            Email = "theodore.lheureux@archlinux.net",
                            EmailConfirmed = false,
                            FirstName = "Theodore",
                            LastName = "l'Heureux",
                            LockoutEnabled = false,
                            NormalizedEmail = "THEODORE.LHEUREUX@ARCHLINUX.NET",
                            NormalizedUserName = "THEODORE",
                            PasswordHash = "AQAAAAEAACcQAAAAEDEmjI7IfVUBkN+PftD2ClWnvRD+wBTjZgwTaQwmaL7hG/sXew5Lo+N0hqz3MrpwEQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7f6bffb7-ccbc-4068-91e5-891676d6d48a",
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
                            Country = "Canada",
                            Name = "Longueuil"
                        },
                        new
                        {
                            Id = 2,
                            Country = "USA",
                            Name = "New York"
                        },
                        new
                        {
                            Id = 3,
                            Country = "China",
                            Name = "Fujiang"
                        },
                        new
                        {
                            Id = 4,
                            Country = "France",
                            Name = "Paris"
                        },
                        new
                        {
                            Id = 5,
                            Country = "Italy",
                            Name = "Rome"
                        },
                        new
                        {
                            Id = 6,
                            Country = "Italy",
                            Name = "Venice"
                        });
                });

            modelBuilder.Entity("Brotherhood_Server.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Briefing")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Codename")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("FeaturedTargetId")
                        .HasColumnType("int");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Contracts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Briefing = "Julie Proulx is using her weight loss program to gain leverage over obese people all over America.Put an end to her manipulative scheme before she uses her customers' money against the Brotherhood.",
                            CityId = 1,
                            Codename = "A Fat Fraud",
                            FeaturedTargetId = 1,
                            IsPublic = true
                        },
                        new
                        {
                            Id = 2,
                            Briefing = "Students in colleges all around the world have begun to worship the dangerous cult of JavaScript.We believe our long-time enemy Valory Sturgeon is behind this ploy to muster allies against our order.Our intelligence suspects she may be using an ancient artifact known as the Aspnet Core to aid her in her quest for absolute control. Find Sturgeon and make this dastardly plan her last. If possible, recover the artifact.",
                            CityId = 1,
                            Codename = "Sturgeon's Last Stand",
                            FeaturedTargetId = 2,
                            IsPublic = true
                        },
                        new
                        {
                            Id = 3,
                            Briefing = "Our long-time collaborator, Paul Clayton, is being kept hostage by the Holy American Inquisition inside their headquarters of the Empire State Building. He is accused of being part of the Furry Fandom. Three men are set to witness against him in the coming days before the Inquisition's Tribunal. Paul is a valuable asset to the Brotherhood, as his status of legend amongst furries grants us a constant stream of fluffy recruits.Eliminate the three witnesses and show the furry community the support our order bestows upon its most loyal supporters.",
                            CityId = 2,
                            Codename = "When Fluff Isn't Enough",
                            IsPublic = true
                        },
                        new
                        {
                            Id = 4,
                            Briefing = "Our contacts in Orient report that ancient and dangerous knowledge from a past civilization has been unearthed in a remote area of rural China. Indeed, traces of a forgotten language known as the Visual Basic have mysteriously emerged after centuries of being removed from this world. Most suspiciously, Valory Sturgeon's closest minion, Joseph de Beloeil, is in charge of analysing the discovered samples. Eliminate De Beloeil and destroy the samples before the world comes to know Visual Basic again.",
                            CityId = 3,
                            Codename = "Bury Evil",
                            IsPublic = false
                        },
                        new
                        {
                            Id = 5,
                            Briefing = "Reports indicate that Didier Paton, loyal member of the Brotherhood, has been captured by Geralt of Rivia, a notorious bounty hunter.While De Rivia's motives for the kidnapping are beyond our knowledge, it cannot but bode ill for Paton. Eliminate de Rivia and make sure his victim comes home safely.",
                            CityId = 4,
                            Codename = "Not the First Time",
                            IsPublic = false
                        });
                });

            modelBuilder.Entity("Brotherhood_Server.Models.ContractTarget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ImageId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("ContractTargets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Julie",
                            LastName = "Proulx"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Valory",
                            LastName = "Sturgeon"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Nazeem",
                            LastName = "Barhoumeter"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Crawford",
                            LastName = "Starrick"
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Charles",
                            LastName = "Lee"
                        },
                        new
                        {
                            Id = 6,
                            FirstName = "Joseph",
                            LastName = "de Beloeil"
                        },
                        new
                        {
                            Id = 7,
                            FirstName = "Geralt",
                            LastName = "of Rivia"
                        },
                        new
                        {
                            Id = 8,
                            FirstName = "Mikael",
                            LastName = "F. Ouhwou"
                        });
                });

            modelBuilder.Entity("ContractContractTarget", b =>
                {
                    b.Property<int>("ContractsId")
                        .HasColumnType("int");

                    b.Property<int>("TargetsId")
                        .HasColumnType("int");

                    b.HasKey("ContractsId", "TargetsId");

                    b.HasIndex("TargetsId");

                    b.ToTable("ContractContractTarget");

                    b.HasData(
                        new
                        {
                            ContractsId = 1,
                            TargetsId = 1
                        },
                        new
                        {
                            ContractsId = 2,
                            TargetsId = 2
                        },
                        new
                        {
                            ContractsId = 3,
                            TargetsId = 3
                        },
                        new
                        {
                            ContractsId = 3,
                            TargetsId = 4
                        },
                        new
                        {
                            ContractsId = 3,
                            TargetsId = 5
                        },
                        new
                        {
                            ContractsId = 4,
                            TargetsId = 6
                        },
                        new
                        {
                            ContractsId = 5,
                            TargetsId = 7
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

            modelBuilder.Entity("ContractContractTarget", b =>
                {
                    b.HasOne("Brotherhood_Server.Models.Contract", null)
                        .WithMany()
                        .HasForeignKey("ContractsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brotherhood_Server.Models.ContractTarget", null)
                        .WithMany()
                        .HasForeignKey("TargetsId")
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
