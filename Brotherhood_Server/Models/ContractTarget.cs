﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class ContractTarget
	{
		public int Id { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(30)]
		public string FirstName { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(30)]
		public string LastName { get; set; }

		[Required]
		[MinLength(1)]
		[MaxLength(50)]
		public string Title { get; set; }

		public string ImageCacheId { get; set; }

		[JsonIgnore]
		public virtual List<Contract> Contracts { get; set; }
	}
}

