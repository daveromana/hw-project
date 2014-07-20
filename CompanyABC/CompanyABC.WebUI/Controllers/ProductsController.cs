﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyABC.Domain.Repositories;
using CompanyABC.Domain.Entities;
using CompanyABC.WebUI.Preferences;
using CompanyABC.WebUI.Models;
using System.Net;
using CompanyABC.WebUI.Localization;
using PagedList;

namespace CompanyABC.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserPreferenceService _userPreferenceService;
        private readonly ILocalizedMessageService _messageService;

        public ProductsController(IProductRepository productRepo, IUserPreferenceService userPrefService, ILocalizedMessageService messageService)
        {
            this._productRepository = productRepo;
            this._userPreferenceService = userPrefService;
            this._messageService = messageService;
        }

        public ViewResult List(int page = 1)
        {
            var products = _productRepository.Products;
            var pageOfProducts = products.OrderBy(product => product.ABCID).ToPagedList(page, _userPreferenceService.Preferences.ProductsPerPage);

            return View(new ProductsViewModel()
            {
                Products = pageOfProducts
            });
        }

        public ActionResult Details(Guid id)
        {
            Product productToView = _productRepository.Products
                .Where(product => product.ABCID == id)
                .FirstOrDefault();

            if (productToView == null)
                return HttpNotFound();

            return View(productToView);
        }

        public ViewResult Create()
        {
            return View("Edit", new Product() { DateCreated = DateTime.Now });
        }

        public ViewResult Edit(Guid id)
        {
            Product productToEdit = _productRepository.Products.FirstOrDefault(product => product.ABCID == id);

            return View(productToEdit);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product);

            _productRepository.SaveProduct(product);
            TempData["results"] = string.Format(_messageService.ProductSaved, product.Title);

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            Product deletedProduct = _productRepository.DeleteProduct(id);

            if (deletedProduct != null)
                TempData["results"] = string.Format(_messageService.ProductDeleted, deletedProduct.Title);

            return RedirectToAction("List");
        }
    }
}