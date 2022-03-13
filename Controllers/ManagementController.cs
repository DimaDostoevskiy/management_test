using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using asu_management.mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Controllers
{
    public class ManagementController : Controller
    {
        private readonly ManagementDbContext _context;
        private readonly ILogger<ManagementController> _logger;

        public ManagementController(ManagementDbContext context, ILogger<ManagementController> logger)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> MainPage()
        {
            return View(await _context.Orders.ToListAsync());
        }
    }
}