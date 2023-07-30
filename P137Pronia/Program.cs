using Microsoft.EntityFrameworkCore;
using P137Pronia.DataAccess;
using P137Pronia.ExtensionServices.Implements;
using P137Pronia.ExtensionServices.Interfaces;
using P137Pronia.Services.Implements;
using P137Pronia.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<ProniaDBContext>(opt => {
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:MSSQL"]);
}); 

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    app.UseHsts();
}

if(app.Environment.IsProduction())
{
    app.UseStatusCodePagesWithRedirects("~/error.html");
}
  
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting(); 

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Slider}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

