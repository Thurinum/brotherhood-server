using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class City
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(80)]
		public string Name { get; set; }

		[Required]
		[MaxLength(80)]
		public string Country { get; set; }
	}
}
