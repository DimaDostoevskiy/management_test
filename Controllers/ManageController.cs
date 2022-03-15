using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.Data;
using asu_management.mvc.Repository;

namespace asu_management.mvc.Controllers
{
    public class ManageController : Controller
    {
        private IOrderRepository _context;
        public ManageController(IOrderRepository context)
        {
            _context = context;
        }
        // GET: Manage
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetAllAsync());
        }
        // GET: Manage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _context.GetByIdWithItemsAsync(id));
        }
        // public IActionResult Create()
        // {
        //     return View();
        // }
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Create([Bind("Date,Name,ProviderId")] Order newOrder)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         _context.Orders.Create(newOrder);

        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(newOrder);
        // }
        // // GET: Manage/Edit/5
        // public async Task<IActionResult> Edit(int id)
        // {
        //     return View(await _context.Orders.GetByIdAsync(id));
        // }
        // // POST: Manage/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Edit(int id, [Bind("Id,Name,Date,ProviderId")] Order order)
        // {
        //     if (id != order.Id)
        //     {
        //         return NotFound();
        //     }

        //     if (!ModelState.IsValid)
        //     {
        //         return View(order);
        //     }

        //     _context.Orders.Update(order);

        //     return RedirectToAction(nameof(Index));
        // }
        // // GET: Manage/Delete/5
        // public async Task<IActionResult> Delete(int id)
        // {
        //     var order = await _context.Orders.GetByIdAsync(id);

        //     if (order == null)
        //     {
        //         return NotFound();
        //     }

        //     await _context.Orders.Delete(order);

        //     return RedirectToAction(nameof(Index));
        // }
    }
}
