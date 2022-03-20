using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.Domain;
using asu_management.mvc.ViewModels;
using asu_management.mvc.PageModel;

namespace asu_management.mvc.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        public OrderController(IOrderRepository context)
        {
            _repository = context;
        }

        #region Index
        // GET: /Order
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new IndexOrderPageModel();
            model.Orders = await _repository.GetAllAsync();
            return View(model);
        }
        // POST: Order/Index/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexOrderPageModel model)
        {
            model.Orders = await _repository.SortAsync(model);
            return View("Index", model);
        }
        #endregion

        #region Details
        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            DetailsOrderPageModel model = new();
            model.Order = await _repository.GetByIdAsync(id);
            return View(model);
        }
        #endregion

        #region Create
        // GET: Order/Create
        public IActionResult Create()
        {
            return View(new OrderViewModel());
        }

        // POST: /Order/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.CreateAsync(model)
                ? RedirectToAction(nameof(Index))
                : RedirectToAction(nameof(Error));
            }
            return View(model);
        }
        #endregion

        #region Edit
        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _repository.GetByIdAsync(id));
        }
        // POST: Order/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.UpdateAsync(model)
                ? RedirectToAction(nameof(Index))
                : RedirectToAction(nameof(Error));
            }
            return View(model);
        }
        #endregion

        #region Delete
        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await _repository.DeleteAsync(id)
            ? RedirectToAction(nameof(Index))
            : RedirectToAction(nameof(Error));
        }
        #endregion

        #region Error
        // GET: Order/Error
        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}
