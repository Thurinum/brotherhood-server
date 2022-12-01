﻿// <auto-generated />
using System;
using Brotherhood_Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Brotherhood_Server.Migrations
{
    [DbContext(typeof(BrotherhoodServerContext))]
    [Migration("20221201043022_owo")]
    partial class owo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                            ConcurrencyStamp = "e488af95-655c-4f92-b06c-bbed70eb2755",
                            Email = "ezio.auditore@firenze.it",
                            EmailConfirmed = false,
                            FirstName = "Ezio",
                            LastName = "Auditore",
                            LockoutEnabled = false,
                            NormalizedEmail = "EZIO.AUDITORE@FIRENZE.IT",
                            NormalizedUserName = "EZIO",
                            PasswordHash = "AQAAAAEAACcQAAAAEPWhKOBQFNLozD9XBGswzIrg2yWCocQU0+josEVqV41YwgUk+Y8lCrTgB7qeC+gm9Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "3ef31b48-9ba7-45ee-bd10-ea2e1d163999",
                            TwoFactorEnabled = false,
                            UserName = "Ezio"
                        },
                        new
                        {
                            Id = "69696969-6969-6969-6969-696969696969",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "07f69b60-71c9-434b-9fe9-c0fea010d33f",
                            Email = "arno.dorian@brotherhood.fr",
                            EmailConfirmed = false,
                            FirstName = "Arno",
                            LastName = "Dorian",
                            LockoutEnabled = false,
                            NormalizedEmail = "ARNO.DORIAN@BROTHERHOOD.fr",
                            NormalizedUserName = "ARNO",
                            PasswordHash = "AQAAAAEAACcQAAAAEF5WKTW7k2nv6az7mh4C3LYzFujMv3kCz+OVb++HC6JELxofUViIjL6MRbyrGCv2Vw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a817ae75-3d0a-476c-82b4-436285d1884d",
                            TwoFactorEnabled = false,
                            UserName = "Arno"
                        },
                        new
                        {
                            Id = "96969696-9696-9696-9696-969696969696",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b73a6d07-1242-46f3-a7ed-6b29f4c3d3b8",
                            Email = "theodore.lheureux@archlinux.net",
                            EmailConfirmed = false,
                            FirstName = "Theodore",
                            LastName = "l'Heureux",
                            LockoutEnabled = false,
                            NormalizedEmail = "THEODORE.LHEUREUX@ARCHLINUX.NET",
                            NormalizedUserName = "THEODORE",
                            PasswordHash = "AQAAAAEAACcQAAAAELib1Q9IIbOyC9ujzRy8OEfrPsl/rcHCekWzuCwP9mKXsxXFpcxmgHdlUc8Q4GKAUQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "000ce693-143b-4de3-a6aa-74da39855601",
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
                        .IsRequired()
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
                            FirstName = "Haytham",
                            LastName = "Kenway"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Rodrigo",
                            LastName = "Borgia"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Shay",
                            LastName = "Cormac"
                        },
                        new
                        {
                            Id = 4,
                            FirstName = "Charles",
                            LastName = "Lee"
                        },
                        new
                        {
                            Id = 5,
                            FirstName = "Valerie",
                            LastName = "Turgeon"
                        },
                        new
                        {
                            Id = 6,
                            FirstName = "Valerie",
                            LastName = "Turgeon"
                        },
                        new
                        {
                            Id = 7,
                            FirstName = "Valerie",
                            LastName = "Turgeon"
                        },
                        new
                        {
                            Id = 8,
                            FirstName = "Valerie",
                            LastName = "Turgeon"
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
                            TargetsId = 1
                        },
                        new
                        {
                            ContractsId = 3,
                            TargetsId = 3
                        },
                        new
                        {
                            ContractsId = 4,
                            TargetsId = 3
                        },
                        new
                        {
                            ContractsId = 5,
                            TargetsId = 4
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