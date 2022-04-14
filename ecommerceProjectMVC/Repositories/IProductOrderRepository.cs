using ecommerceProjectMVC.Models;
using System.Collections.Generic;

namespace ecommerceProjectMVC.Repositories
{
    public interface IProductOrderRepository
    {
        List<ProductOrder> GetAll();
        ProductOrder GetById(int ProductOrderId);
        int Insert(Product product, Order order, int quantity);
        int Update(int ProductOrderId, ProductOrder newpo);
        int Delete(int ProductOrderId);
    }
}
