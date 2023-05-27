using EcommerceMVCProjec.Repository.Interface;
using EcommerceProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceMVCProjec.Controllers
{
    public class AssignOrderToDeliveryBoyController : Controller
    {
        private readonly IOrderDeliveryBoyRepository orderDeliveryBoy;
        public AssignOrderToDeliveryBoyController(IOrderDeliveryBoyRepository orderDeliveryBoy)
        {
            this.orderDeliveryBoy = orderDeliveryBoy;
        }

        // POST: OrderController/Create

        public async Task<ActionResult> AssignOrderToDeliver(OrderDeliveryModel orderDelivery)
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                var UserId = Int32.Parse(uId);
                orderDelivery.createdBy = UserId;

                var ord = await orderDeliveryBoy.AssignOrderToDeliveryBoy(orderDelivery);
                if (ord > 0)
                {
                
                    return RedirectToAction("GetAllOrders", "Order");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        
        public async Task<ActionResult> OrderDeliveryredToCustomer(long OrderId)
        {
            try
            {
                OrderDeliveryModel orderDelivery = new OrderDeliveryModel();
                var uId = HttpContext.Session.GetString("userId");
                var UserId = Int32.Parse(uId);
                orderDelivery.modifiedBy = UserId;
                orderDelivery.OrderId=OrderId;

                var ord = await orderDeliveryBoy.OrderDeliveryredToCustomer(orderDelivery);
                if (ord > 0)
                {

                    return RedirectToAction("GetAllOrders", "Order");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> AcceptOrRejectTheOrder(OrderDeliveryModel orderDelivery)
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                var UserId = Int32.Parse(uId);
                orderDelivery.modifiedBy = UserId;

                var ord = await orderDeliveryBoy.AcceptOrRejectTheOrder(orderDelivery);
                if (ord > 0)
                {

                    return RedirectToAction("GetAllOrders", "Order");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public async Task<ActionResult> OrderDeliveryBoy()
        {
            try
            {
                var uId = HttpContext.Session.GetString("userId");
                var UserId = Int32.Parse(uId);
               

                var ord = await orderDeliveryBoy.GetOrderAcceptedByDeliveryBoy(UserId);
               
                return View(ord);
            }
            catch
            {
                return View();
            }
        }
    }
   


}
