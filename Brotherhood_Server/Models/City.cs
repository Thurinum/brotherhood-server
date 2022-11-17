using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class City
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsPublic { get; set; }

		public List<Assassin> Assassins { get; set; }
	}
}
