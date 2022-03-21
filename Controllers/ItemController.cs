using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.Domain;
using asu_management.mvc.ViewModels;
using Serilog;

namespace asu_management.mvc.Controllers
{
    public class ItemController : Controller
    {
        private ItemRepository _repository;
        public ItemController(ItemRepository repository)
        {
            _repository = repository;
        }
        #region Create
        // GET: Item/CreateItem/5
        [HttpGet]
        public IActionResult CreateItem(int id)
        {
            var model = new ItemViewModel();
            model.OrderId = id;
            return View(model);
        }

        // POST: Item/CreateItem/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(ItemViewModel model) //TODO: Add Bind
        {
            if (ModelState.IsValid)
            {
                return await _repository.CreateItemAsync(model)
                ? Redirect($@"/Order/Details/{model.OrderId}")
                : Redirect(@"/Order/Error");
            }

            Log.Warning(@" NO VALID /Item/CreateItem");

            return View(model);
        }
        #endregion

        #region Edit
        // GET: Item/EditItem/5
        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            ItemViewModel model = await _repository.GetItemByIdAsync(id);
            return View(model);
        }
        // POST: Item/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.UpdateItemAsync(model)
                ? Redirect($@"/Order/Details/{model.OrderId}")
                : Redirect(@"/Order/Error");
            }
            Log.Warning(@" NO VALID /Item/EditItem");
            
            return View(model);
        }
        #endregion

        #region Delete
        // GET: Item/Deletetem/5
        [HttpGet]
        public async Task<IActionResult> DeleteItem(ItemViewModel model)
        {
            return await _repository.RemoveAsync(model.Id)
            ? Redirect($@"/Order/Index")
            : Redirect(@"/Order/Error");
        }
        #endregion
    }
}