using asu_management.mvc.Data;
using asu_management.mvc.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace asu_management.mvc.Domain
{
    public class Mapper
    {
        internal static OrderViewModel MapOrderToModel(Order order)
        {
            var model = new OrderViewModel()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.Provider.Id,
                ProviderName = order.Provider.Name
            };

            List<ItemViewModel> list = new();

            foreach (var item in order.Items)
            {
                list.Add(MapItemToModel(item));
            }

            model.Items = list.ToArray();

            return model;
        }
        internal static async Task<Order> MapModelToOrderAsync(OrderViewModel model, ManagementDbContext context)
        {
            var provider = await context.Providers
                .FirstOrDefaultAsync(x => x.Id == model.ProviderId);

            if (provider == null)
            {
                return null;
            }

            var order = new Order()
            {
                Number = model.Number,
                Date = model.Date,
                Provider = provider
            };

            return order;
        }
        internal static async Task<OrderItem> MapModelToItemAsync(
                ItemViewModel model, ManagementDbContext context)
        {
            var order = await context.Orders
                                        .FirstOrDefaultAsync(x => x.Id == model.OrderId);
            if(order == null)
            {
                return null;
            }
            var item = new OrderItem()
            {
                Name = model.Name,
                Unit = model.Unit,
                Quantity = model.Quantity,
                Order = order
            };
            return item;
        }
        internal static ItemViewModel MapItemToModel(OrderItem item)
        {
            var model = new ItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Unit = item.Unit,
                Quantity = item.Quantity,
                OrderId = item.Order.Id,
                OrderNumber = item.Order.Number
            };
            return model;
        }
        internal static ProviderViewModel MapProviderToModel(Provider provider)
        {
            var model = new ProviderViewModel()
            {
                Id = provider.Id,
                Name = provider.Name
            };
            return model;
        }
    }
}