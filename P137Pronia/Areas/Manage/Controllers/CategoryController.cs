﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P137Pronia.Services.Interfaces;

namespace P137Pronia.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin,Editor")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name)) return BadRequest();
            await _service.Create(name);
            return View();
        }
    }
}

