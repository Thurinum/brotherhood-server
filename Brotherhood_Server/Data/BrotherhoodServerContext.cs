using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Brotherhood_Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Brotherhood_Server.Data
{
	public class BrotherhoodServerContext : IdentityDbContext<Assassin>
	{
		public BrotherhoodServerContext(DbContextOptions<BrotherhoodServerContext> options) : base(options) { }

		public DbSet<Brotherhood_Server.Models.City> City { get; set; }
	}
}
