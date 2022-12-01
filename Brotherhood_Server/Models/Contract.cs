using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class Contract
	{
		public int Id { get; set; }
		public int? FeaturedTargetId { get; set; }

		[Required]
		public int CityId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Codename { get; set; }

		[Required]
		[MaxLength(1000)]
		public string Briefing { get; set; }

		[Required]
		public bool IsPublic { get; set; }

		[JsonIgnore]
		public virtual List<Assassin> Assassins { get; set; }

		[JsonIgnore]
		public virtual List<ContractTarget> Targets { get; set; }
	}
}
