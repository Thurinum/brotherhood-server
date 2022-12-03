using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brotherhood_Server.Data
{
	public class BrotherhoodServerContext : IdentityDbContext<Assassin>
	{
		public BrotherhoodServerContext(DbContextOptions<BrotherhoodServerContext> options) : base(options) { }

		public DbSet<Contract> Contracts { get; set; }
		public DbSet<ContractTarget> ContractTargets { get; set; }
		public DbSet<City> Cities { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Contract>().HasData(
				new Contract()
				{
					Id = 1,
					CoverTargetId = 1,
					CityId = 1,
					Codename = "A Fat Fraud",
					IsPublic = true,
					Briefing = "Julie Proulx is using her weight loss program to gain leverage over obese people all over America. " +
							   "Put an end to her manipulative scheme before she uses her customers' money against the Brotherhood."
				},
				new Contract()
				{
					Id = 2,
					CoverTargetId = 2,
					CityId = 1,
					Codename = "Sturgeon's Last Stand",
					IsPublic = true,
					Briefing = "Students in colleges all around the world have begun to worship the dangerous cult of JavaScript. " +
							   "We believe our long-time enemy Valory Sturgeon is behind this ploy to muster allies against our order. " +
							   "Our intelligence suspects she may be using an ancient artifact known as the Aspnet Core to aid her in her quest " +
							   "for absolute control. Find Sturgeon and make this dastardly plan her last. If possible, recover the artifact."
				},
				new Contract()
				{
					Id = 3,
					CityId = 2,
					Codename = "When Fluff Isn't Enough",
					IsPublic = true,
					Briefing = "Our long-time collaborator, Paul Clayton, is being kept hostage by the Holy American Inquisition inside " +
							   "their headquarters of the Empire State Building. He is accused of being part of the Furry Fandom. Four men " +
							   "are set to witness against him in the coming days before the Inquisition's Tribunal. Paul is a valuable asset to " +
							   "the Brotherhood, as his status of legend amongst furries grants us a constant stream of fluffy recruits. " +
							   "Eliminate the three witnesses and show the furry community the support our order bestows upon its most loyal supporters."
				},
				new Contract()
				{
					Id = 4,
					CityId = 3,
					Codename = "Bury Evil",
					IsPublic = false,
					Briefing = "Our contacts in Orient report that ancient and dangerous knowledge from a past civilization has been unearthed in a remote area of rural China. " +
							   "Indeed, traces of a forgotten language known as the Visual Basic have mysteriously emerged after centuries of being removed from this world. " +
							   "Most suspiciously, Valory Sturgeon's closest minion, Joseph de Beloeil, is in charge of analysing the discovered samples. Eliminate De Beloeil " +
							   "and destroy the samples before the world comes to know Visual Basic again."
				},
				new Contract()
				{
					Id = 5,
					CityId = 4,
					Codename = "Not the First Time",
					IsPublic = false,
					Briefing = "Reports indicate that Didier Paton, loyal member of the Brotherhood, has been captured by Geralt of Rivia, a notorious bounty hunter. " +
							   "While De Rivia's motives for the kidnapping are beyond our knowledge, it cannot but bode ill for Paton. Eliminate de Rivia and make sure " +
							   "his victim comes home safely."
				},
				new Contract()
				{
					Id = 6,
					CityId = 6,
					Codename = "Being Faster than the Other Guy",
					IsPublic = true,
					Briefing = "The vile Nazeem Barhoumeter is a notorious plague to the citizens of Venice. His meddling with currency counterfeiting " +
							"and illegal trading of pocket monsters has made him a target of the Brotherhood. Eliminate Barhoumeter and his associates " +
							"before he can cause any more trouble."

				}
			);

			builder.Entity<ContractTarget>().HasData(
				new ContractTarget() { Id = 1, FirstName = "Julie", LastName = "Proulx", Title = "Entrepreneur" },
				new ContractTarget() { Id = 2, FirstName = "Valory", LastName = "Sturgeon", Title = "Cult Leader" },
				new ContractTarget() { Id = 3, FirstName = "Nazeem", LastName = "Barhoumeter", Title = "Arrant Knave" },
				new ContractTarget() { Id = 4, FirstName = "Crawford", LastName = "Starrick", Title = "Grandmaster of the Templar Order" },
				new ContractTarget() { Id = 5, FirstName = "Charles", LastName = "Lee", Title = "Knight of the Templar Order" },
				new ContractTarget() { Id = 6, FirstName = "Joseph", LastName = "de Beloeil", Title = "French Aristocrat" },
				new ContractTarget() { Id = 7, FirstName = "Geralt", LastName = "of Rivia", Title = "Monster Hunter" },
				new ContractTarget() { Id = 8, FirstName = "Anonymous", LastName = "Traitor", Title = "Dastardly Betrayer" }
			);

			builder.Entity<City>().HasData(
				new City() { Id = 1, Name = "Longueuil", Country = "Canada" },
				new City() { Id = 2, Name = "New York", Country = "USA" },
				new City() { Id = 3, Name = "Fujiang", Country = "China" },
				new City() { Id = 4, Name = "Paris", Country = "France" },
				new City() { Id = 5, Name = "Rome", Country = "Italy" },
				new City() { Id = 6, Name = "Venice", Country = "Italy" }
			);

			builder.Entity<Contract>()
				.HasMany(c => c.Targets)
				.WithMany(t => t.Contracts)
				.UsingEntity(e => e.HasData(
					new { ContractsId = 1, TargetsId = 1 },
					new { ContractsId = 2, TargetsId = 2 },
					new { ContractsId = 3, TargetsId = 3 },
					new { ContractsId = 3, TargetsId = 4 },
					new { ContractsId = 3, TargetsId = 5 },
					new { ContractsId = 3, TargetsId = 8 },
					new { ContractsId = 4, TargetsId = 6 },
					new { ContractsId = 5, TargetsId = 7 }
				));

			// Arno
			PasswordHasher<Assassin> hasher = new();
			// Theodore
			Assassin erhion = new()
			{
				Id = "96969696-9696-9696-9696-969696969696",
				UserName = "Theodore",
				FirstName = "Theodore",
				LastName = "l'Heureux",
				NormalizedUserName = "THEODORE",
				Email = "theodore.lheureux@archlinux.net",
				NormalizedEmail = "THEODORE.LHEUREUX@ARCHLINUX.NET"
			};
			erhion.PasswordHash = hasher.HashPassword(erhion, "dioxus420");

			Assassin arno = new()
			{
				Id = "69696969-6969-6969-6969-696969696969",
				UserName = "Arno",
				FirstName = "Arno",
				LastName = "Dorian",
				NormalizedUserName = "ARNO",
				Email = "arno.dorian@brotherhood.fr",
				NormalizedEmail = "ARNO.DORIAN@BROTHERHOOD.fr"
			};
			arno.PasswordHash = hasher.HashPassword(arno, "elise69");

			// Ezio
			Assassin ezio = new()
			{
				Id = "11111111-1111-1111-1111-111111111111",
				UserName = "Ezio",
				FirstName = "Ezio",
				LastName = "Auditore",
				NormalizedUserName = "EZIO",
				Email = "ezio.auditore@firenze.it",
				NormalizedEmail = "EZIO.AUDITORE@FIRENZE.IT"
			};
			ezio.PasswordHash = hasher.HashPassword(ezio, "requiescat in pace");

			builder.Entity<Assassin>().HasData(ezio, arno, erhion);
			builder.Entity<Contract>()
				.HasMany(c => c.Assassins)
				.WithMany(a => a.Contracts)
				.UsingEntity(e => e.HasData(
					new { ContractsId = 1, AssassinsId = erhion.Id },
					new { ContractsId = 2, AssassinsId = arno.Id },
					new { ContractsId = 3, AssassinsId = arno.Id },
					new { ContractsId = 4, AssassinsId = ezio.Id },
					new { ContractsId = 5, AssassinsId = erhion.Id }
				));
		}
	}
}
