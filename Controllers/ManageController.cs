#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asu_management.mvc.Data;
using asu_management.mvc.Models;
using asu_management.mvc.Services;

namespace asu_management.mvc.Controllers
{
    public class ManageController : Controller
    {
        private readonly Repository _repository;
        public ManageController(ManagementDbContext context)
        {
            _repository = new Repository(context);
        }

        // GET: Manage
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllOrders());
        }

        //     // GET: Manage/Details/5
        //     public async Task<IActionResult> Details(int? id)
        //     {
        //         if (id == null)
        //         {
        //             return NotFound();
        //         }

        //         var orderModel = await _context.Orders
        //             .FirstOrDefaultAsync(m => m.Id == id);
        //         if (orderModel == null)
        //         {
        //             return NotFound();
        //         }

        //         return View(orderModel);
        //     }

        // GET: Manage/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Date")] OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                if (await _repository.Add(order))
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(order);
            }
            return View(order);
        }

        //     // GET: Manage/Edit/5
        //     public async Task<IActionResult> Edit(int? id)
        //     {
        //         if (id == null)
        //         {
        //             return NotFound();
        //         }

        //         var orderModel = await _context.OrderModel.FindAsync(id);
        //         if (orderModel == null)
        //         {
        //             return NotFound();
        //         }
        //         return View(orderModel);
        //     }

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
