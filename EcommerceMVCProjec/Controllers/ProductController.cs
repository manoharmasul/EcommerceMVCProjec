using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductAsyncRepository productAsync;
        public ProductController(IProductAsyncRepository productAsync)
        {
            this.productAsync = productAsync;   
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var product = await productAsync.GetAllProducts();

            return View(product);
        }
        public async Task<ActionResult> OrderContSales()
        {
            var product = await productAsync.ProductsOrderCount();

            return View(product);
        }
        public async Task<ActionResult> GetproductByType(string category)
        {

            var product = await productAsync.GetProductByType(category);

            return View(product);
        }
        public async Task<ActionResult> GetproductBySearchText(string searchtext)
        {

            var product = await productAsync.GetProductBySearchText(searchtext);

            return View(product);
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var productdetails=await productAsync.GetProductById(id);   
            return View(productdetails);
        }
        // GET: ProductController/Create
        public ActionResult InsertImages()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertImages(Demo addproduct)
        {
            try
            {
                var product = await productAsync.InsertDemo(addproduct);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // GET: ProductController/Create
        public ActionResult AddNewProduct()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewProduct(ProductAdd addproduct)
        {
            try
            {
                var product=await productAsync.AddNewProduct(addproduct);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
