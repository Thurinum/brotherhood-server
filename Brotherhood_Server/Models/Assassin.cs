using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class Assassin : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		
		public virtual List<Contract> Contracts { get; set; }
	}
}
