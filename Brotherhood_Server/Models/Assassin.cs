using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class Assassin : IdentityUser
	{
		public virtual List<City> Cities { get; set; }
	}
}
