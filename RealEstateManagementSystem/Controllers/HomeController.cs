using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateManagementSystem.Data;

namespace RealEstateManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext context;

        public HomeController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var property = context.RealEstateProperty.OrderByDescending(p => p.Id).ToList();
            return View(property);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Listing()
        {
            return View();
        }
    }
}
