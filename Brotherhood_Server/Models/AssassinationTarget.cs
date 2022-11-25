using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	  public class AssassinationTarget
	  {
			public int Id { get; set; }
			public int CityId { get; set; }

			[Required]
			[MaxLength(30)]
			public string FirstName { get; set; }

			[Required]
			[MaxLength(30)]
			public string LastName { get; set; }

			[EmailAddress]
			public string EmailAddress { get; set; }
	  }
}

