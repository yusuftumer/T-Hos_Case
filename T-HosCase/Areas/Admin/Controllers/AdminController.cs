using Dashboard.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using T_HosCase.Context;
using T_HosCase.Entities;
using T_HosCase.Helpers;
using T_HosCase.Models.CategoryModels;
using T_HosCase.Models.ProductModels;
using T_HosCase.Models.PropertyModels;

namespace T_HosCase.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class AdminController : Controller
    {
        private readonly Case_DbContext _context;
        private readonly IFileUpload _fileUpload;
        public AdminController(Case_DbContext context, IFileUpload fileUpload)
        {
            _context = context;
            _fileUpload = fileUpload;
        }
        public IActionResult CategoryList()
        {
            var data = _context.Categories.Where(x => x.IsDeleted == false);
            return View(data);
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpGet("EditCategory/{CategoryId}")]
        public IActionResult EditCategory(int CategoryId)
        {
            var category = _context.Categories.Where(x => x.CategoryId == CategoryId && x.IsDeleted == false).FirstOrDefault();
            if (category is not null)
            {
                var returnCategory = new CategoryDetailDto();
                returnCategory.CreatedDate = category.CreatedDate;
                returnCategory.CategoryId = category.CategoryId;
                returnCategory.ParentCategoryId = category.ParentCategoryId;
                returnCategory.CategoryName = category.CategoryName;
                returnCategory.CreatorUserId = category.CreatorUserId;
                return View(returnCategory);//Burada direkt category değişkenide döndürülüebilir.
            }
            return Redirect("/admin/Admin/CategoryList");
        }
        [HttpPost]
        public IActionResult AddCategory(AddCategoryModel model)
        {
            var cookieValue = Request.Cookies["security-token"];
            if (cookieValue != null)
            {
                JwtSecurityTokenHandler tokenHandler = new();
                JwtSecurityToken? token = tokenHandler.ReadJwtToken(cookieValue);
                var userid = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier.ToString());
                var category = new Category();
                category.CategoryName = model.CategoryName;
                category.ParentCategoryId = model.ParentCategoryId;
                category.IsDeleted = false;
                category.CreatedDate = DateTime.Now;
                category.CreatorUserId = Convert.ToInt32(userid);
                _context.Categories.Add(category);
                return Redirect("/admin/Admin/CategoryList");
            }
            return Redirect("/admin/User/Login");
        }
        [HttpPost]
        public IActionResult EditCategory(EditCategoryModel model)
        {
            var category = _context.Categories.Where(x => x.CategoryId == model.CategoryId && x.IsDeleted == false).FirstOrDefault();
            if (category != null)
            {
                category.CategoryName = model.CategoryName;
                category.ParentCategoryId = model.ParentCategoryId;
                _context.Categories.Update(category);
                return Redirect("/admin/Admin/CategoryList");
            }
            return Redirect("/admin/Admin/CategoryList");
        }
        public IActionResult ProductList()
        {
            var data = _context.Products.Where(x => x.IsDeleted == false);
            return View(data);
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(IFormFile Image, AddProductModel model)
        {
            var cookieValue = Request.Cookies["security-token"];
            if (cookieValue != null)
            {
                JwtSecurityTokenHandler tokenHandler = new();
                JwtSecurityToken? token = tokenHandler.ReadJwtToken(cookieValue);
                var userid = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier.ToString());
                var file = "";
                if (Image != null)
                {
                    file = _fileUpload.FileUploads(Image);
                    if (file is not null)
                    {
                        model.ImagePath = file;
                    }
                }
                var product = new Product();
                product.CategoryId = model.CategoryId;
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.ImagePath = model.ImagePath;
                product.IsDeleted = false;
                product.CreatedDate = DateTime.Now;
                product.CreatorUserId = Convert.ToInt32(userid);
                _context.Products.Add(product);
                return Redirect("/admin/Admin/ProductList");
            }
            return Redirect("/admin/User/Login");
        }
        [HttpGet("EditProduct/{ProductId}")]
        public IActionResult EditProduct(int ProductId)
        {
            var product = _context.Products.Where(x => x.ProductId == ProductId && x.IsDeleted == false).FirstOrDefault();
            if (product is not null)
            {
                var returnProduct = new ProductDetailDto();
                returnProduct.ProductId = product.ProductId;
                returnProduct.ProductName = product.ProductName;
                returnProduct.ImagePath = product.ImagePath;
                returnProduct.Price = product.Price;
                returnProduct.CategoryId = product.CategoryId;
                return View(returnProduct);//Burada direkt product değişkenide döndürülüebilir.
            }
            return Redirect("/admin/Admin/ProductList");
        }
        [HttpPost]
        public IActionResult EditProduct(IFormFile Image, EditProductModel model)
        {
            var product = _context.Products.Where(x => x.ProductId == model.ProductId && x.IsDeleted == false).FirstOrDefault();
            if (product != null)
            {
                var file = "";
                if (Image != null)
                {
                    file = _fileUpload.FileUploads(Image);
                    if (file is not null)
                    {
                        model.ImagePath = file;
                    }
                }
                product.ProductName = model.ProductName;
                product.CategoryId = model.CategoryId;
                product.ImagePath = model.ImagePath;
                product.Price = model.Price;
                _context.Products.Update(product);
                return Redirect("/admin/Admin/ProductList");
            }
            return Redirect("/admin/Admin/ProductList");
        }
        public IActionResult PropertyList()
        {
            var properties = _context.Properties.Where(x => x.IsDeleted == false).ToList();
            return View(properties);
        }
        [HttpPost]
        public IActionResult AddProperty(AddPropertyModel model)
        {
            var cookieValue = Request.Cookies["security-token"];
            if (cookieValue != null)
            {
                JwtSecurityTokenHandler tokenHandler = new();
                JwtSecurityToken? token = tokenHandler.ReadJwtToken(cookieValue);
                var userid = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier.ToString());
                var property = new Property();
                property.Value = model.Value;
                property.Key = model.Key;
                _context.Properties.Add(property);
                return Redirect("/admin/Admin/PropertyList");
            }
            return Redirect("/admin/User/Login");
        }
        [HttpGet("ProductProperties/{ProductId}")]
        public IActionResult ProductProperties(int ProductId)
        {
            var fproduct = _context.Products.Where(x => x.ProductId == ProductId).FirstOrDefault();
            var properties = _context.ProductProperties.Where(x => x.ProductId == ProductId).ToList();
            if (properties is not null)
            {
                var returnProperties = new List<ProductPropertiesDto>();
                foreach (var property in properties)
                {
                    var addProperties = new ProductPropertiesDto();
                    var fproperty = _context.Properties.FirstOrDefault(x => x.PropertyId == property.PropertyId);

                    if (fproperty != null)
                    {
                        addProperties.PropertyValue = fproperty.Value;
                        addProperties.PropertyKey = fproperty.Key;
                        addProperties.ProductName = fproduct.ProductName;
                        returnProperties.Add(addProperties);
                    }
                }
                return Json(returnProperties);
            }
            return Json(false);
        }
    }
}
