using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace P137Pronia.Models
{
	public class ApppUser:IdentityUser
	{
		[Required]
		public string Fullname { get; set; }
		public DateTime BirthDate { get; set; }
	}
}


