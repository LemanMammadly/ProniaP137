using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P137Pronia.DataAccess;
using P137Pronia.Services.Interfaces;
using P137Pronia.ViewModels.HomeVMs;

namespace P137Pronia.Controllers;

public class HomeController : Controller
{
    private readonly ProniaDBContext _context;
    private readonly ISliderService _sliderService;
    private readonly IProductService _productService;
    public HomeController(IProductService productService, ISliderService sliderService)
    {
        _productService = productService;
        _sliderService = sliderService;
    }
    public async Task<IActionResult> Index()
    {
        HomeVM vm = new HomeVM
        {
            Sliders = await _sliderService.GetAll(),
            Products = await _productService.GetAll(false)
        };
         return View(vm); 
    }
}

