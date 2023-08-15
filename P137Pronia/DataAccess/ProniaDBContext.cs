using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P137Pronia.Models;

namespace P137Pronia.DataAccess
{
	public class ProniaDBContext:IdentityDbContext
	{
        public ProniaDBContext(DbContextOptions options):base(options){}
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ApppUser> ApppUsers { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasIndex(p => p.Name).IsUnique();
            base.OnModelCreating(modelBuilder);
        }
    }
}



