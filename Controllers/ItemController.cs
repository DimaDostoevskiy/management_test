using Microsoft.AspNetCore.Mvc;
using asu_management.mvc.Domain;
using asu_management.mvc.ViewModels;
using Serilog;

namespace asu_management.mvc.Controllers
{
    public class ItemController : Controller
    {
        private ItemRepository _repository;
        private const string errorPath = @"/Order/Error";
        public ItemController(ItemRepository repository)
        {
            _repository = repository;
        }
        
        #region Create
        // GET: Item/CreateItem/5
        [HttpGet]
        public IActionResult CreateItem(int id)
        {
            ItemViewModel model = new();
            model.OrderId = id;
            return View(model);
        }

        // POST: Item/CreateItem/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.CreateItemAsync(model)
                ? Redirect($"/Order/Details/{model.OrderId}")
                : Redirect(errorPath);
            }

            Log.Information("   NO VALID /Item/CreateItem");

            return View(model);
        }
        #endregion

        #region Edit
        // GET: Item/EditItem/5
        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            ItemViewModel model = await _repository.GetItemByIdAsync(id);

            return (model == null) 
            ? Redirect(errorPath)
            : View(model);
        }
        // POST: Item/EditItem/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await _repository.EditItemAsync(model)
                ? Redirect($@"/Order/Details/{model.OrderId}")
                : Redirect(errorPath);
            }
            return View(model);
        }
        #endregion

        #region Delete
        // GET: Item/Deletetem/5
        [HttpGet]
        public async Task<IActionResult> DeleteItem(int id)
        {
            return await _repository.DeleteItemAsync(id)
            ? Redirect($@"/Order/Index")
            : Redirect(errorPath);
        }
        #endregion
    }
}