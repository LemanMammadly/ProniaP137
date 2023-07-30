using System;
using Microsoft.EntityFrameworkCore;
using P137Pronia.Models;

namespace P137Pronia.DataAccess
{
	public class ProniaDBContext:DbContext
	{
        public ProniaDBContext(DbContextOptions options):base(options){}
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}


