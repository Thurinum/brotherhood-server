using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Role { get; set; }

		public virtual List<Contract> Contracts { get; set; }
	}
}
