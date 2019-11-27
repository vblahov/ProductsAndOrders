using ProductsAndOrders.Models;

namespace ProductsAndOrders.DataStorage
{
    public interface IProductLibraryRepository : ILibraryRepository<ProductViewModel>
    {
        ProductViewModel SearchByName(string name);
        UpdatePriceMessage UpdatePrice(int? id, decimal? price);
        AddProductMessage Add(AddProductRequest request);
    }
}