using System;
namespace P137Pronia.Models
{
	public class UserToken:BaseEntity
	{
		public string ApppUserId { get; set; }
		public string Key { get; set; }
		public DateTime SendDate { get; set; }
		public ApppUser ApppUser { get; set; }
	}
}



