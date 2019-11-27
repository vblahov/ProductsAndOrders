using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProductsAndOrders.Models;

namespace ProductsAndOrders.DataStorage
{
    public interface ILibraryRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        IDeleteMessage Delete(int? id);
    }
}