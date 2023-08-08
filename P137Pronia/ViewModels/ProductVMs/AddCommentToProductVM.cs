using System;
namespace P137Pronia.ViewModels.ProductVMs
{
	public record AddCommentToProductVM
	{
		public int ProductId { get; set; }
		public int? ParentId { get; set; }
		public string Comment { get; set; }
	}
}


