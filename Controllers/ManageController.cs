using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.ViewModels;
using asu_management.mvc.Domain;

namespace asu_management.mvc.Controllers
{
    public class ManageController : Controller
    {
        private IRepository<OrderViewModel> _repository;
        public ManageController(IRepository<OrderViewModel> context)
        {
            _repository = context;
        }

        // GET: /Manage
        public async Task<IActionResult> Index()
        {
            var model = new OrderViewModel();
            model.Orders = await _repository.GetAllAsync();
            return View(model);
        }
        // POST: /Manage/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SortOrders(OrderViewModel model)
        {
            model.Orders = await _repository.SortAsync(model);
            return View("Index", model);
        }
        // GET: Manage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _repository.GetByIdAsync(id));
        }
        // GET: Manage/Create
        public IActionResult Create()
        {
            return View(new OrderViewModel());
        }

        // POST: /Manage/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {

                return await _repository.Create(model) 
                ? RedirectToAction(nameof(Index)) 
                : RedirectToAction(nameof(Error));
            }
            return View(model);
        }
        // GET: Manage/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _repository.GetByIdAsync(id));
        }
        // POST: Manage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Date,ProviderId")]  OrderViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _repository.Update(model);

            return RedirectToAction(nameof(Index));
        }

        // GET: Manage/Delete/5
        public async Task<IActionResult> Delete(OrderViewModel orderModel)
        {
            return await _repository.Delete(orderModel) ? 
                RedirectToAction(nameof(Index)) : RedirectToAction(nameof(Error)) ;
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
