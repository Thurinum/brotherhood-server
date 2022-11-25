using Microsoft.EntityFrameworkCore;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Data
{
	public class BrotherhoodServerContext : IdentityDbContext<Assassin>
	{
		public BrotherhoodServerContext(DbContextOptions<BrotherhoodServerContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<City>().HasData(
				new City() { Id = 1, Name = "Florence", IsPublic = true },
				new City() { Id = 2, Name = "Rome", IsPublic = true },
				new City() { Id = 3, Name = "Paris", IsPublic = true },
				new City() { Id = 4, Name = "Venice", IsPublic = true },
				new City() { Id = 5, Name = "Longueuil", IsPublic = false }
			);

			builder.Entity<AssassinationTarget>().HasData(
				new AssassinationTarget() { Id = 1, CityId = 5, FirstName = "Haytham", LastName = "Kenway", EmailAddress = "haytham.kenway@order.org" },
				new AssassinationTarget() { Id = 2, CityId = 2, FirstName = "Rodrigo", LastName = "Borgia", EmailAddress = "rodrigo.borgia@vatican.va" },
				new AssassinationTarget() { Id = 3, CityId = 5, FirstName = "Shay", LastName = "Cormac", EmailAddress = "shay.cormac@order.org" },
				new AssassinationTarget() { Id = 4, CityId = 5, FirstName = "Charles", LastName = "Lee", EmailAddress = "charles.lee@order.org" },
				new AssassinationTarget() { Id = 5, CityId = 3, FirstName = "Valerie", LastName = "Turgeon", EmailAddress = "valerie.turgeon@abstergo.org" }
			);

			// Arno
			PasswordHasher<Assassin> hasher = new PasswordHasher<Assassin>();
			Assassin arno = new Assassin()
			{
				Id = "69696969-6969-6969-6969-696969696969",
				UserName = "Arno",
				NormalizedUserName = "ARNO",
				Email = "arno.dorian@brotherhood.org",
				NormalizedEmail = "ARNO.DORIAN@BROTHERHOOD.ORG"
			};
			arno.PasswordHash = hasher.HashPassword(arno, "elise69");

			// Theodore
			Assassin erhion = new Assassin()
			{
				Id = "96969696-9696-9696-9696-969696969696",
				UserName = "Theodore",
				NormalizedUserName = "THEODORE",
				Email = "theodore.lheureux@archlinux.net",
				NormalizedEmail = "THEODORE.LHEUREUX@ARCHLINUX.NET"
			};
			erhion.PasswordHash = hasher.HashPassword(erhion, "dioxus420");

			// Theodore
			Assassin ezio = new Assassin()
			{
				Id = "11111111-1111-1111-1111-111111111111",
				UserName = "Ezio",
				NormalizedUserName = "EZIO",
				Email = "ezio.auditore@firenze.it",
				NormalizedEmail = "EZIO.AUDITORE@FIRENZE.IT"
			};
			ezio.PasswordHash = hasher.HashPassword(ezio, "requiescat in pace");

			builder.Entity<Assassin>().HasData(ezio,	arno, erhion);
			builder.Entity<City>()
				.HasMany(c => c.Assassins)
				.WithMany(a => a.Cities)
				.UsingEntity(e => e.HasData(
					new { CitiesId = 1, AssassinsId = ezio.Id },
					new { CitiesId = 2, AssassinsId = ezio.Id },
					new { CitiesId = 3, AssassinsId = arno.Id },
					new { CitiesId = 4, AssassinsId = ezio.Id },
					new { CitiesId = 5, AssassinsId = erhion.Id }
				));
		}



		public DbSet<City> City { get; set; }
	}
}
