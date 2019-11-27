using ProductsAndOrders.Models;

namespace ProductsAndOrders.DataStorage
{
    public interface IOrderLibraryRepository : ILibraryRepository<OrderViewModel>
    {
        Orders SearchByRecipientName(string name);
        void  UpdateDestinationCity(int id, string city);
        int Add(AddOrderRequest request);
    }
}