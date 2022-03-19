using asu_management.mvc.Data;
using asu_management.mvc.ViewModels;

namespace asu_management.mvc.Domain
{
    public class Mapper
    {
        internal static OrderViewModel MapOrderToModel(Order order)
        {
            OrderViewModel viewModel = new OrderViewModel()
            {
                Id = order.Id,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId,
                ProviderName = order.Provider.Name
            };
            
            List<ItemViewModel> list = new();

            foreach (var item in order.Items)
            {
                list.Add(MapItemToModel(item));
            }

            viewModel.Items = list.ToArray();

            return viewModel;
        }
        internal static Order MapModelToOrder(OrderViewModel viewModel)
        {
            var order = new Order()
            {
                Number = viewModel.Number,
                Date = viewModel.Date,
                ProviderId = viewModel.ProviderId
            };
            return order;
        }
        internal static OrderItem MapModelToItem(ItemViewModel viewModel)
        {
            var orderItem = new OrderItem()
            {
                Name = viewModel.Name,
                Unit = viewModel.Unit,
                Quantity = viewModel.Quantity,
            };
            return orderItem;
        }
        internal static ItemViewModel MapItemToModel(OrderItem item)
        {
            var order = new ItemViewModel()
            {
                Id = item.Id,
                Name = item.Name,
                Unit = item.Unit,
                Quantity = item.Quantity,
                OrderId = item.OrderId,
                OrderNumber = item.Order.Number
            };
            return order;
        }
    }
}