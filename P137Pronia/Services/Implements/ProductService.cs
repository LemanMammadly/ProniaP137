using System;
using Microsoft.EntityFrameworkCore;
using P137Pronia.DataAccess;
using P137Pronia.ExtensionServices.Interfaces;
using P137Pronia.Models;
using P137Pronia.Services.Interfaces;
using P137Pronia.ViewModels.ProductVMs;

namespace P137Pronia.Services.Implements
{ 
    public class ProductService : IProductService 
    {
        readonly ProniaDBContext _context;
        private readonly IWebHostEnvironment _env;
        readonly IFileService _fileService;
        public ProductService(ProniaDBContext context, IFileService fileService, IWebHostEnvironment env)
        {
            _context = context;
            _fileService = fileService;
            _env = env;
        }

        public IQueryable<Product> GetTable { get => _context.Set<Product>(); }

        public async Task Create(CreateProductVM productVm)
        {

            Product entity = new Product()
            {
                Name = productVm.Name,
                Description = productVm.Description,
                Discount = productVm.Discount,
                Price = productVm.Price,
                Raiting = productVm.Raiting,
                StockCount = productVm.StockCount,
                MainImage = await _fileService.UploadAsync(productVm.MainImageFile, Path.Combine
                ("assets", "imgs", "products"))
            };
            if(productVm.ImageFiles != null)
            {
                List<ProductImage> imgs = new();
                foreach (var file in productVm.ImageFiles)
                {
                    string fileName = await _fileService.UploadAsync(file, Path.Combine("assets", "imgs", "products"));
                    imgs.Add(new ProductImage
                    {
                        Name = fileName
                    });
                }
                entity.ProductImages = imgs;
            }
            if(productVm.HoverImageFile !=null)
            {
                entity.HoverImage = await _fileService.UploadAsync(productVm.HoverImageFile, Path.Combine
                ("assets", "imgs", "products"));
            }
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? id)
        {
            var entity = await GetById(id);
            _context.Remove(entity);
            _fileService.Delete(entity.MainImage);
            if(entity.HoverImage != null)
            {
                _fileService.Delete(entity.HoverImage);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAll(bool takeAll)
        {
            if(takeAll)
            {
                return await _context.Products.ToListAsync();
            }
            return await _context.Products.Where(p=>p.IsDeleted==false).ToListAsync();
        }

        public async Task<Product> GetById(int? id)
        {
            if (id < 1 || id == null) throw new ArgumentException();
            var entity=await _context.Products.FindAsync(id);
            if (entity == null) throw new NullReferenceException();
            return entity;
        }

        public async Task SoftDelete(int? id)
        {
            var entity = await GetById(id);
            entity.IsDeleted = !entity.IsDeleted;
            await _context.SaveChangesAsync();
        }

        public async Task Update(UpdateProductVM productVm)
        {
            var entity = await GetById(productVm.Id);
            entity.Name = productVm.Name;
            entity.Description = productVm.Description;
            entity.Price = productVm.Price;
            entity.Discount = productVm.Discount;
            entity.StockCount = productVm.StockCount;
            entity.Raiting = productVm.Raiting;
            await _context.SaveChangesAsync();
        }
    }
}

