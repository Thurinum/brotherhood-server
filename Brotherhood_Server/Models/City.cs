﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class City
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		public bool IsPublic { get; set; }

		public virtual List<Assassin> Assassins { get; set; }
	}
}
