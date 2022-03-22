using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.Domain;
using asu_management.mvc.PageModel;

namespace asu_management.mvc.Controllers
{
    public class OrderController : Controller
    {
        private const string errorPath = @"/Order/Error";
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
            var model = new IndexPageModel();
            model.Orders = await _repository.GetAllOrdersAsync();
            model.SelectProviders = await _repository.GetListProvaidersAsync();
            return View(model);
        }
        // POST: Order/Index/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IndexPageModel model)
        {
            model.Orders = await _repository.SortOrderAsync(model);
            model.SelectProviders = await _repository.GetListProvaidersAsync();
            return View(model);
        }
        #endregion

        #region Details
        // GET: Order/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            OrderDetailsPageModel model = new();

            model.Order = await _repository.GetOrderByIdAsync(id);

            var select = await _repository.GetSelectListsAsync(model.Order.Id);
            
            model.SortNames = select[1];
            model.SortUnits = select[0]; // Возможно, это не лучшее решение, но добавить new SelectListItem никак не выходит =(

            return (model.Order == null)
            ? Redirect(errorPath)
            : View(model);
        }
        // GET: Order/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(OrderDetailsPageModel model) // Bind property?
        {
            model.Order = await _repository.GetOrderByIdAsync(model.Order.Id);
            var select = await _repository.GetSelectListsAsync(model.Order.Id);
            model.SortNames = select[1];
            model.SortUnits = select[0];

            model.Order.Items = _repository.SortItems
                (model.Order.Items, model.SortName, model.SortUnit, model.StartSortQuantity, model.EndSortQuantity);

            return (model.Order == null)
            ? Redirect(errorPath)
            : View(model);
        }
        #endregion

        #region Create
        // GET: Order/Create
        public async Task<IActionResult> Create()
        {
            OrderCreatePageModel model = new();
            model.Providers = await _repository.GetListProvaidersAsync();

            return (model.Providers == null)
            ? Redirect(errorPath)
            : View(model);
        }

        // POST: /Order/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreatePageModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.CreateOrderAsync(model.Order)
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
            pageModel.SelectProviders = await _repository.GetListProvaidersAsync();
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
                return await _repository.EditOrderAsync(model.Order)
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
            return await _repository.DeleteOrderAsync(id)
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
