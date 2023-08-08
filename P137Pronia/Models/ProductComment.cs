using System;
namespace P137Pronia.Models
{
	public class ProductComment:BaseEntity
	{
		public string AppUserId { get; set; }
		public int ProductId { get; set; }
		public string Comment { get; set; }
		public DateTime PostedTime { get; set; }
		public ApppUser? ApppUser { get; set; }
		public int? ParentId { get; set; }
		public ProductComment? Parent { get; set; }
		public ICollection<ProductComment>? Children { get; set; }
        public Product? Product { get; set; }
		public bool IsDeleted { get; set; }
	}
}





