using ProductsAndOrders.DataStorage;

namespace ProductsAndOrders.Services
{
    public class OrderService : OrderLibraryRepository, IOrderService
    {
        public OrderService(LibraryContext context) : base(context)
        {
        }
    }
}