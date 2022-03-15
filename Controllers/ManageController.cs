#nullable disable
using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.Data;
using asu_management.mvc.Services;

namespace asu_management.mvc.Controllers
{
    public class ManageController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public ManageController(ManagementDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        // GET: Manage
        public IActionResult Index()
        {
            return View(_unitOfWork.Orders.GetAll());
        }

        // GET: Manage/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Date,Name,ProviderId")] Order newOrder)
        {
            if (!ModelState.IsValid)
            {
                _unitOfWork.Orders.Create(newOrder);
                return RedirectToAction(nameof(Index));
            }
            return View(newOrder);
        }

        // GET: Manage/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //     // POST: Manage/Edit/5
        //     // To protect from overposting attacks, enable the specific properties you want to bind to.
        //     // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //     [HttpPost]
        //     [ValidateAntiForgeryToken]
        //     public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Date,ProviderId")] OrderModel orderModel)
        //     {
        //         if (id != orderModel.Id)
        //         {
        //             return NotFound();
        //         }

        //         if (ModelState.IsValid)
        //         {
        //             try
        //             {
        //                 _context.Update(orderModel);
        //                 await _context.SaveChangesAsync();
        //             }
        //             catch (DbUpdateConcurrencyException)
        //             {
        //                 if (!OrderModelExists(orderModel.Id))
        //                 {
        //                     return NotFound();
        //                 }
        //                 else
        //                 {
        //                     throw;
        //                 }
        //             }
        //             return RedirectToAction(nameof(Index));
        //         }
        //         return View(orderModel);
        //     }

        //     // GET: Manage/Delete/5
        //     public async Task<IActionResult> Delete(int? id)
        //     {
        //         if (id == null)
        //         {
        //             return NotFound();
        //         }

        //         var orders = await _context.Orders
        //             .FirstOrDefaultAsync(m => m.Id == id);
        //         if (orders == null)
        //         {
        //             return NotFound();
        //         }

        //         return View(orders);
        //     }

        //     // POST: Manage/Delete/5
        //     [HttpPost, ActionName("Delete")]
        //     [ValidateAntiForgeryToken]
        //     public async Task<IActionResult> DeleteConfirmed(int id)
        //     {
        //         var orderModel = await _context.OrderModel.FindAsync(id);
        //         _context.OrderModel.Remove(orderModel);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }

        //     private bool OrderModelExists(int id)
        //     {
        //         return _context.OrderModel.Any(e => e.Id == id);
        //     }
    }
}
