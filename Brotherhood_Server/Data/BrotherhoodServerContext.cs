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
				new City() { Id = 1, Name = "New York", IsPublic = true },
				new City() { Id = 2, Name = "London", IsPublic = true },
				new City() { Id = 3, Name = "Paris", IsPublic = true },
				new City() { Id = 4, Name = "Rome", IsPublic = true },
				new City() { Id = 5, Name = "Tokyo", IsPublic = true }
			);

			PasswordHasher<Assassin> hasher = new PasswordHasher<Assassin>();
			Assassin assassin = new Assassin()
			{
				Id = "11111111-1111-1111-1111-111111111111",
				UserName = "admin",
				NormalizedUserName = "ADMIN",
				Email = "admin@mail.com",
				NormalizedEmail = "ADMIN@MAIL.COM"
			};
			assassin.PasswordHash = hasher.HashPassword(assassin, "password");
			builder.Entity<Assassin>().HasData(assassin);

			builder.Entity<City>()
				.HasMany(c => c.Assassins)
				.WithMany(a => a.Cities)
				.UsingEntity(e => e.HasData(new { CitiesId = 1, AssassinsId = assassin.Id }));
		}



		public DbSet<City> City { get; set; }
	}
}
