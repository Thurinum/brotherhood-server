﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Brotherhood_Server.Migrations
{
    public partial class OwO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverTargetId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Codename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Briefing = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTargets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageCacheId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTargets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractUser",
                columns: table => new
                {
                    AssassinsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContractsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractUser", x => new { x.AssassinsId, x.ContractsId });
                    table.ForeignKey(
                        name: "FK_ContractUser_AspNetUsers_AssassinsId",
                        column: x => x.AssassinsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractUser_Contracts_ContractsId",
                        column: x => x.ContractsId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractContractTarget",
                columns: table => new
                {
                    ContractsId = table.Column<int>(type: "int", nullable: false),
                    TargetsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractContractTarget", x => new { x.ContractsId, x.TargetsId });
                    table.ForeignKey(
                        name: "FK_ContractContractTarget_Contracts_ContractsId",
                        column: x => x.ContractsId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractContractTarget_ContractTargets_TargetsId",
                        column: x => x.TargetsId,
                        principalTable: "ContractTargets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2", "3564e880-c935-4912-8fff-d919e30bb3ca", "Assassin", "ASSASSIN" },
                    { "1", "3cd887ee-c53c-48ab-b817-f2d7661e7a4c", "Mentor", "MENTOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "96969696-9696-9696-9696-969696969696", 0, "96bb9e7b-6bea-4727-b059-649505850b34", "theodore.lheureux@archlinux.net", false, "Theodore", "l'Heureux", false, null, "THEODORE.LHEUREUX@ARCHLINUX.NET", "THEODORE", "AQAAAAEAACcQAAAAEMYpUCwifUkQCupnZFDYnfqjt7iTn3tgdUbfRNCY7mxyS4Ibqg5j8hlI9OhoLMBxsg==", null, false, "4da3fdad-1571-4dd5-b43f-c8ba16ac228d", false, "Theodore" },
                    { "69696969-6969-6969-6969-696969696969", 0, "138ef4cf-26d4-49b9-a6d2-13098549203a", "arno.dorian@brotherhood.fr", false, "Arno", "Dorian", false, null, "ARNO.DORIAN@BROTHERHOOD.fr", "ARNO", "AQAAAAEAACcQAAAAEL5qPxmCbiL12qdmt/lG22G9eVOIB/pkr+e/zkfACbuSJ7h/A22EFeps5pu4oJ9Jfw==", null, false, "a49ee044-fc6f-4843-8556-0899ddca0ef4", false, "Arno" },
                    { "11111111-1111-1111-1111-111111111111", 0, "df42a1ea-857f-473d-ac6a-b634b4ef81ad", "ezio.auditore@firenze.it", false, "Ezio", "Auditore", false, null, "EZIO.AUDITORE@FIRENZE.IT", "EZIO", "AQAAAAEAACcQAAAAEIr1OkmU+BoXQmUfa4tALFfLc7jfUELhLIJdVGCAAupE2ABurnVnevue7hrWDJXMFA==", null, false, "8764e39d-598d-47ac-8c17-8fa9bc8df3c5", false, "Ezio" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Country", "Name" },
                values: new object[,]
                {
                    { 1, "Canada", "Longueuil" },
                    { 2, "USA", "New York" },
                    { 3, "England", "London" },
                    { 4, "China", "Fujiang" },
                    { 5, "France", "Paris" },
                    { 6, "Italy", "Rome" }
                });

            migrationBuilder.InsertData(
                table: "ContractTargets",
                columns: new[] { "Id", "FirstName", "ImageCacheId", "LastName", "Title" },
                values: new object[,]
                {
                    { 4, "Charles", null, "Lee", "Knight of the Templar Order" },
                    { 9, "Geralt", null, "of Rivia", "Renowned Monster Hunter" },
                    { 8, "Joseph", null, "de Beloeil", "Corrupt Aristocrat" },
                    { 7, "Roger", null, "Y. Smith", "Industrial Magnate and Entrepreneur" },
                    { 6, "Crawford", null, "Starrick", "Grandmaster of the Templar Order" },
                    { 1, "Julie", null, "Proulx", "Entrepreneur" },
                    { 3, "Haytham", null, "Kenway", "Grandmaster of the Templar Order" },
                    { 2, "Valory", null, "Sturgeon", "Leader of the Cult of the Asp" },
                    { 5, "Shay", null, "Cormac", "Knight of the Templar Order" }
                });

            migrationBuilder.InsertData(
                table: "Contracts",
                columns: new[] { "Id", "Briefing", "CityId", "Codename", "CoverTargetId", "IsPublic" },
                values: new object[,]
                {
                    { 6, "Reports indicate that Didier Paton, loyal member of the Brotherhood, has been captured by Geralt of Rivia, a notorious bounty hunter. While De Rivia's motives for the kidnapping are beyond our knowledge, it cannot but bode ill for Paton. Eliminate de Rivia and make sure his victim comes home safely.", 5, "Not the First Time", 0, false },
                    { 5, "Our contacts in Orient report that ancient and dangerous knowledge from a past civilization has been unearthed in a remote area of rural China. Indeed, traces of a forgotten language known as the Visual Basic have mysteriously emerged after centuries of being removed from this world. Most suspiciously, Valory Sturgeon's closest minion, Joseph de Beloeil, is in charge of analysing the discovered samples. Eliminate De Beloeil and destroy the samples before the world comes to know Visual Basic again.", 4, "Bury Evil", 0, false },
                    { 4, "The vile Crawford Starrick is a notorious plague to the citizens of London. His meddling with currency counterfeiting and illegal trading of pocket monsters has left thousands in the streets and many struggling with financial security. He and his left-hand Roger Smith are using the stock market to gain control over all of London's industries. Make sure to give them a share of what the Brotherhood is capable of when provoked. Eliminate Starrick and his associate.", 3, "Being Faster than the Other Guy", 6, true },
                    { 3, "Our long-time collaborator, Paul Clayton, is being kept hostage by the Holy American Inquisition inside their headquarters of the Empire State Building. He is accused of being part of the Furry Fandom. Four men are set to witness against him in the coming days before the Inquisition's Tribunal. Paul is a valuable asset to the Brotherhood, as his status of legend amongst furries grants us a constant stream of fluffy recruits. Eliminate the three witnesses and show the furry community the support our order bestows upon its most loyal supporters.", 2, "When Fluff Isn't Enough", 0, true },
                    { 2, "Students in colleges all around the world have begun to worship the dangerous cult of JavaScript. We believe our long-time enemy Valory Sturgeon is behind this ploy to muster allies against our order. Our intelligence suspects she may be using an ancient artifact known as the Aspnet Core to aid her in her quest for absolute control. Find Sturgeon and make this dastardly plan her last. If possible, recover the artifact.", 1, "Sturgeon's Last Stand", 2, true },
                    { 1, "Julie Proulx is using her weight loss program to gain leverage over obese people all over America. Put an end to her manipulative scheme before she uses her customers' money against the Brotherhood.", 1, "A Fat Fraud", 1, true }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "69696969-6969-6969-6969-696969696969" },
                    { "1", "11111111-1111-1111-1111-111111111111" },
                    { "2", "96969696-9696-9696-9696-969696969696" }
                });

            migrationBuilder.InsertData(
                table: "ContractContractTarget",
                columns: new[] { "ContractsId", "TargetsId" },
                values: new object[,]
                {
                    { 6, 9 },
                    { 3, 8 },
                    { 4, 7 },
                    { 5, 8 },
                    { 3, 5 },
                    { 3, 4 },
                    { 3, 3 },
                    { 5, 2 },
                    { 2, 2 },
                    { 4, 6 },
                    { 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "ContractUser",
                columns: new[] { "AssassinsId", "ContractsId" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", 4 },
                    { "11111111-1111-1111-1111-111111111111", 5 },
                    { "69696969-6969-6969-6969-696969696969", 2 },
                    { "69696969-6969-6969-6969-696969696969", 3 },
                    { "96969696-9696-9696-9696-969696969696", 1 },
                    { "96969696-9696-9696-9696-969696969696", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractContractTarget_TargetsId",
                table: "ContractContractTarget",
                column: "TargetsId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractUser_ContractsId",
                table: "ContractUser",
                column: "ContractsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "ContractContractTarget");

            migrationBuilder.DropTable(
                name: "ContractUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ContractTargets");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Contracts");
        }
    }
}
