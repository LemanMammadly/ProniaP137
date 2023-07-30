 using System;
using P137Pronia.Models;
using P137Pronia.ViewModels.ProductVMs;

namespace P137Pronia.Services.Interfaces
{
	public interface IProductService
	{
		public Task<List<Product>> GetAll(bool takeAll);
		public Task<Product> GetById(int? id);
		public Task Create(CreateProductVM productVm);
		public Task Update(UpdateProductVM productVm);
		public Task Delete(int? id);
		public Task SoftDelete(int? id);
		IQueryable<Product> GetTable { get; }
	}
}


