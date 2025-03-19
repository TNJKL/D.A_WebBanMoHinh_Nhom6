using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebSiteBanMoHinh.Areas.Admin.Repository;
using WebSiteBanMoHinh.Models;
using WebSiteBanMoHinh.Repository;

namespace WebSiteBanMoHinh.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IEmailSender _emailSender;

        public CheckoutController(IEmailSender emailSender, DataContext context)
        {
            _dataContext = context;
            _emailSender = emailSender;

        }
        public async Task<IActionResult> Checkout()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if(userEmail == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                var orderCode = Guid.NewGuid().ToString();
                var orderItem = new OrderModel();
                orderItem.OrderCode = orderCode;
                orderItem.UserName = userEmail;
                orderItem.Status = 1;
                orderItem.CreatedDate = DateTime.Now;
                _dataContext.Add(orderItem);
                _dataContext.SaveChanges();
                List<CartItemModel> cartItems = HttpContext.Session.GetJSon<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
                foreach(var cart in cartItems)
                {
                    var orderDetails = new OrderDetails();
                    orderDetails.UserName = userEmail;
                    orderDetails.OrderCode = orderCode;
                    orderDetails.ProductId = cart.ProductId;
                    orderDetails.Price = cart.Price;
                    orderDetails.Quantity = cart.Quantity;
                    _dataContext.Add(orderDetails);
                    _dataContext.SaveChanges();
                }
                HttpContext.Session.Remove("Cart");
                var receiver = userEmail;
                var subject = "Đặt hàng thành công";
                var message = "Đặt hàng thành công, trải nghiệm dịch vụ nhé.";
                await _emailSender.SendEmailAsync(receiver, subject, message);
                TempData["success"] = "Checkout thành công , vui lòng chờ duyệt đơn hàng";
                return RedirectToAction("Index", "Cart");
            }
                return View();
        }
    }
}
