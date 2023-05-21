using EcommerceProject.Models;
using EcommerceProject.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartAsyncRepository cartAsync;
        public CartController(ICartAsyncRepository cartAsync)
        {
            this.cartAsync = cartAsync; 
        }
        // GET: CartController
        public async Task<ActionResult> ViewCart()
        {
            var uId = HttpContext.Session.GetString("userId");
            var  UserId = Int32.Parse(uId);
            var result = await cartAsync.GetCartProducts(UserId);
            return View(result);
        }

        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public async Task<ActionResult> AddToCart(long productsid)
        {
            try
            {
                Cart cartadd=new Cart();

                var uId = HttpContext.Session.GetString("userId");
                cartadd.UserId = Int32.Parse(uId);
                cartadd.createdBy = Int32.Parse(uId);
                cartadd.ProductId=productsid;

                var result = await cartAsync.AddToCart(cartadd);
                return RedirectToAction(nameof(ViewCart));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
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

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
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
