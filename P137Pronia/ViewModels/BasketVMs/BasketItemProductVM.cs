using System;
using P137Pronia.Models;

namespace P137Pronia.ViewModels.BasketVMs
{
	public record  BasketItemProductVM
	{
		public Product Product { get; set; }
		public int Count { get; set; }
	}
}


