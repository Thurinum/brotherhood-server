using Microsoft.EntityFrameworkCore;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

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
					CityId = 5,
					Codename = "Codename: LoneEagle",
					IsPublic = true,
					Briefing = "The dastarly Haytham Kenway is disrupting peace and hindering freedom of the people of America. " +
					"Bring him and his acolytes down and ensure justice is brought to the people."
				},
				new Contract() { Id = 2, CityId = 2, Codename = "Pope", IsPublic = true, Briefing = "The Pope is a threat to the people of Italy. Bring him down and ensure justice is brought to the people." },
				new Contract() { Id = 3, CityId = 5, Codename = "Rogue", IsPublic = true, Briefing = "The dastarly Shay Cormac is disrupting peace and hindering freedom of the people of America. Bring him down and ensure justice is brought to the people." },
				new Contract() { Id = 4, CityId = 5, Codename = "Dastardly", IsPublic = true, Briefing = "The dastarly Charles lee is disrupting peace and hindering freedom of the people of America. Bring him down and ensure justice is brought to the people." },
				new Contract() { Id = 5, CityId = 3, Codename = "ViewModel", IsPublic = false, Briefing = "The dastarly Julie Pro is disrupting peace and hindering freedom of the people of America. Bring him down and ensure justice is brought to the people." }
			);

			builder.Entity<City>().HasData(
				new City() { Id = 1, Name = "Florence", Country = "Italy" },
				new City() { Id = 2, Name = "Rome", Country = "Italy" },
				new City() { Id = 3, Name = "Paris", Country = "France" },
				new City() { Id = 4, Name = "Venice", Country = "Italy" },
				new City() { Id = 5, Name = "Longueuil", Country = "Canada" }
			);

			builder.Entity<ContractTarget>().HasData(
				new ContractTarget() { Id = 1, FirstName = "Haytham", LastName = "Kenway" },
				new ContractTarget() { Id = 2, FirstName = "Rodrigo", LastName = "Borgia" },
				new ContractTarget() { Id = 3, FirstName = "Shay", LastName = "Cormac" },
				new ContractTarget() { Id = 4, FirstName = "Charles", LastName = "Lee" },
				new ContractTarget() { Id = 5, FirstName = "Valerie", LastName = "Turgeon" },
				new ContractTarget() { Id = 6, FirstName = "Valerie", LastName = "Turgeon" },
				new ContractTarget() { Id = 7, FirstName = "Valerie", LastName = "Turgeon" },
				new ContractTarget() { Id = 8, FirstName = "Valerie", LastName = "Turgeon" }
			);

			// Arno
			PasswordHasher<Assassin> hasher = new();
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
					new { ContractsId = 1, AssassinsId = ezio.Id },
					new { ContractsId = 2, AssassinsId = ezio.Id },
					new { ContractsId = 3, AssassinsId = arno.Id },
					new { ContractsId = 4, AssassinsId = ezio.Id },
					new { ContractsId = 5, AssassinsId = erhion.Id }
				));

			builder.Entity<Contract>()
					.HasMany(c => c.Targets)
					.WithMany(t => t.Contracts)
					.UsingEntity(e => e.HasData(
						new { ContractsId = 1, TargetsId = 1 },
						new { ContractsId = 2, TargetsId = 1 },
						new { ContractsId = 3, TargetsId = 3 },
						new { ContractsId = 4, TargetsId = 3 },
						new { ContractsId = 5, TargetsId = 4 }
					));
		}
	}
}
