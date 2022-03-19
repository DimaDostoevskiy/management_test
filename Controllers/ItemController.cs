using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.Domain;
using asu_management.mvc.ViewModels;

namespace asu_management.mvc.Controllers
{
    public class ItemController : Controller
    {
        private IItemRepository _repository;
        public ItemController(IItemRepository context)
        {
            _repository = context;
        }

        // GET: Item/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _repository.GetItemByIdAsync(id));
        }
        // POST: Item/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.UpdateItemAsync(model)
                ? Redirect($"/Order/Details/{model.OrderId}")
                : Redirect("/Order/Error");
            }
            return View(model);
        }

        // GET: Item/CreateItem/5
        [HttpGet]
        public IActionResult CreateItem(int orderId)
        {
            var model = new ItemViewModel();
            model.OrderId = orderId;
            return View(model);
        }

        // POST: /Order/Create/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.CreateItemAsync(model)
                ? Redirect($"/Order/Details/{model.OrderId}")
                : Redirect("/Order/Error");
            }
            return View(model);
        }



    }
}