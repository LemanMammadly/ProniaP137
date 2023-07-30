using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P137Pronia.Extensions;
using P137Pronia.Services.Interfaces;
using P137Pronia.ViewModels.ProductVMs;

namespace P137Pronia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        public async  Task<IActionResult> Index()
        {
            return View(await _service.GetAll(true));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (productVM.MainImageFile != null)
            {
                    if (!productVM.MainImageFile.IsTypeValid("image"))
                    {
                    ModelState.AddModelError("MainImageFile", "Wrong file type");
                    }
                if(!productVM.MainImageFile.IsSizeValid(2))
                   {
                    ModelState.AddModelError("MainImageFile", "File max size is 2mb");
                   }
            }

            if(productVM.HoverImageFile != null)
            {
                if (!productVM.HoverImageFile.IsTypeValid("image"))
                {
                    ModelState.AddModelError("HoverImageFile", "Wrong file type");
                }
                if (!productVM.HoverImageFile.IsSizeValid(2))
                {
                    ModelState.AddModelError("HoverImageFile", "File max size is 2mb");
                }
            }

            if(productVM.ImageFiles !=null)
            {
                foreach (var img in productVM.ImageFiles)
                {
                    if (!img.IsTypeValid("image"))
                    {
                        ModelState.AddModelError("ImageFiles", "Wrong file type " + img.FileName);
                    }
                    if (!img.IsSizeValid(2))
                    {
                        ModelState.AddModelError("ImageFiles", "File max size is 2mb " + img.FileName);
                    }
                }
            }

            if (!ModelState.IsValid) return View();
            await _service.Create(productVM);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChangeStatus(int? id)
        {
            await _service.SoftDelete(id);
            TempData["IsDeleted"] = true;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            return View(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProductVM productVM)
        {
            await _service.Update(productVM);
            return RedirectToAction(nameof(Index));
        }
    }
}

