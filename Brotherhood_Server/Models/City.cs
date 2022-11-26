using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class City
	{
		public int Id { get; set; }

		public int CoverTargetId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		public bool IsPublic { get; set; }

		[JsonIgnore]
		public virtual List<Assassin> Assassins { get; set; }

		[JsonIgnore]
		public virtual List<AssassinationTarget> Targets { get; set; }
	}
}
