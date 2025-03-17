using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteBanMoHinh.Repository;

namespace WebSiteBanMoHinh.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private readonly DataContext _dataContext;
        public RoleController(DataContext context)
        {
            _dataContext = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Roles.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
