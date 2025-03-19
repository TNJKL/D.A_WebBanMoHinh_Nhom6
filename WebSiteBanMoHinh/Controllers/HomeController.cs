using System.Diagnostics;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebSiteBanMoHinh.Models;
using WebSiteBanMoHinh.Repository;



namespace WebSiteBanMoHinh.Controllers;

public class HomeController : Controller
{   //localizer

   
    private readonly DataContext _dataContext;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger , DataContext context)
    {
        _logger = logger;
        _dataContext = context;
       


    }
    public IActionResult ChangeLang(string culture)
    {
        Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1)
        });
        return Redirect(Request.Headers["Referer"].ToString());
    }
    public IActionResult Index()
    {
       
        var products = _dataContext.Products.Include("Category").Include("Brand").ToList();
        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statuscode)
    {
        if (statuscode == 404)
        {
            return View("NotFound");
        }
        else{
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
  
}
