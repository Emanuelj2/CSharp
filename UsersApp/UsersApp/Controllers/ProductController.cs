using Microsoft.AspNetCore.Mvc;
using UsersApp.Models;
using UsersApp.Models.ViewModels;
using UsersApp.Service;

namespace UsersApp.Controllers
{
    
    [Route("products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }



    }
}
