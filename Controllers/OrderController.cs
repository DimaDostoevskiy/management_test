using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.Domain;
using asu_management.mvc.ViewModels;
using asu_management.mvc.PageModel;
using AutoMapper;

namespace asu_management.mvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderRepository _repository;
        public OrderController(OrderRepository repository)
        {
            _repository = repository;
        }

        #region Index
        // GET: /Order
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new IndexOrderPageModel();
            model.Orders = await _repository.GetAllOrdersAsync();
            model.Providers = await _repository.GetListProvaidersAsync();
            return View(model);
        }
        // POST: Order/Index/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexOrderPageModel model)
        {
            model.Orders = await _repository.SortOrderAsync(model);
            model.Providers = await _repository.GetListProvaidersAsync();
            return View("Index", model);
        }
        #endregion

        #region Details
        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            DetailsOrderPageModel model = new();
            model.Order = await _repository.GetOrderByIdAsync(id);
            return View(model);
        }
        #endregion

        #region Create
        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            OrderViewModel model = new();
            model.Providers = await _repository.GetListProvaidersAsync();
            return View(model);
        }

        // POST: /Order/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.CreateOrderAsync(model)
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
            OrderEditPageModel pageModel = new();
            pageModel.Providers = await _repository.GetListProvaidersAsync();
            pageModel.Order = await _repository.GetOrderByIdAsync(id);
            return View(pageModel);
        }
        // POST: Order/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderEditPageModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.OrderUpdateAsync(model)
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
            return await _repository.RemoveAsync(id)
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
