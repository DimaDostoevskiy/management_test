using asu_management.mvc.Data;
using asu_management.mvc.Models;

namespace asu_management.mvc.Domain
{
    public class Mapper
    {
        internal static OrderModel MapOrderToModel(Order order)
        {
            OrderModel viewModel = new OrderModel()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId,
                ProviderName = order.Provider.Name
            };
            
            List<ItemModel> list = new();

            foreach (var item in order.Items)
            {
                list.Add(MapItemToItemModel(item));
            }

            viewModel.Items = list.ToArray();

            return viewModel;
        }
        internal static Order MapModelToOrder(OrderModel viewModel)
        {
            var order = new Order()
            {
                Number = viewModel.Number,
                Date = viewModel.Date,
                ProviderId = viewModel.ProviderId
            };
            return order;
        }
        internal static OrderItem MapItemModelToItem(ItemModel viewModel)
        {
            var OrderItem = new OrderItem()
            {
                Name = viewModel.Name,
                Unit = viewModel.Unit,
                Quantity = viewModel.Quantity,
                OrderId = viewModel.OrderId
            };
            return OrderItem;
        }
        internal static ItemModel MapItemToItemModel(OrderItem item)
        {
            var order = new ItemModel()
            {
                Id = item.Id,
                Name = item.Name,
                Unit = item.Unit,
                Quantity = item.Quantity,
                OrderId = item.OrderId
            };
            return order;
        }
    }
}