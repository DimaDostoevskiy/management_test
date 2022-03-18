using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.ViewModels;
using asu_management.mvc.Domain;
using asu_management.mvc.Models;

namespace asu_management.mvc.Controllers
{
    public class OrderController : Controller
    {
        private IRepository<OrderModel> _repository;
        public OrderController(IRepository<OrderModel> context)
        {
            _repository = context;
        }

        // GET: /Order
        public async Task<IActionResult> Index()
        {
            var model = new OrderViewModel();
            model.Orders = await _repository.GetAllAsync();
            return View(model);
        }
        // POST: /Order/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SortOrders(OrderViewModel model)
        {
            model.Orders = await _repository
                .SortAsync(model.ProviderId, model.SortNumber, model.StartSortDate, model.EndSortDate);

            return View("Index", model);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ItemViewModel ItemModel = new();
            ItemModel.Order = await _repository.GetByIdAsync(id);

            return View(ItemModel);
        }
        // GET: Order/Create
        public IActionResult Create()
        {
            return View(new OrderViewModel());
        }

        // POST: /Order/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.CreateAsync(model)
                ? RedirectToAction(nameof(Index))
                : RedirectToAction(nameof(Error));
            }
            return View(model);
        }
        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _repository.GetByIdAsync(id));
        }
        // POST: Order/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.UpdateAsync(model)
                ? RedirectToAction(nameof(Index))
                : RedirectToAction(nameof(Error));
            }
            return View(model);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await _repository.DeleteAsync(id)
            ? RedirectToAction(nameof(Index))
            : RedirectToAction(nameof(Error));
        }
        // GET: Order/Error
        public IActionResult Error()
        {
            return View();
        }
    }
}
