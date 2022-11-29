using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Brotherhood_Server.Models
{
	public class ContractShareDTO
	{
		[Required]
		public string AssassinName { get; set; }

		[Required]
		public int ContractId { get; set; }
	}
}
